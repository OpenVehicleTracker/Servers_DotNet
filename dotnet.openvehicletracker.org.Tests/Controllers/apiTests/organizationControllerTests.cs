using dotnet.openvehicletracker.org.Controllers.api;
using dotnet.openvehicletracker.org.Models.Entities;
using dotnet.openvehicletracker.org.Tests.Controllers.apiTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace dotnet.openvehicletracker.org.Tests.Controllers
{
    [TestClass]
    public class organizationControllerTests : BaseApiTest
    {

        [TestMethod]
        public void OrganizationGetTest()
        {
            string testOrgName = "orgName";

            var controller = new organizationController(new MockContext());

            IEnumerable<Organization> resultArray = controller.Get();
            Assert.IsNotNull(resultArray);
            Assert.AreEqual(0, resultArray.Count());

            CreateOrganization(controller, testOrgName);

            resultArray = controller.Get();
            Assert.IsNotNull(resultArray);
            Assert.IsInstanceOfType(resultArray, typeof(System.Data.Entity.IDbSet<Organization>));
            Assert.AreEqual(1, resultArray.Count());

            string actualName = resultArray.First().Name;
            string expectedName = testOrgName;
            Assert.AreEqual(expectedName, actualName);

            Organization result = controller.Get(testOrgName);
            actualName = result.Name;
            expectedName = testOrgName;
            Assert.AreEqual(expectedName, actualName);

            result = controller.Get("none");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void OrganizationCreateTest()
        {
            string testOrgName = "TestOrgName";
            string testOrgName2 = "TestOrgName Too";

            var controller = new organizationController(new MockContext());

            IEnumerable<Organization> result;

            CreateOrganization(controller, testOrgName);
            CreateOrganization(controller, testOrgName2);

            result = controller.Get();
            Assert.AreEqual(2, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testOrgName));
            Assert.IsTrue(result.Any(m => m.Name == testOrgName2));
        }

        [TestMethod]
        public void OrganizationCreateDuplicateNamesTest()
        {
            string testOrgNameValid = "validOrgName";
            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            var controller = new organizationController(new MockContext());

            IEnumerable<Organization> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            CreateOrganization(controller, testOrgNameValid);
            expectedCode = System.Net.HttpStatusCode.Conflict;
            expectedStatus = "exists";
            CreateOrganization(controller, testOrgNameValid, expectedCode, expectedStatus);

            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            string actualName = result.FirstOrDefault().Name;
            string expectedName = testOrgNameValid;
            Assert.AreEqual(expectedName, actualName);

        }

        [TestMethod]
        public void OrganizationCreateReservedNamesTest()
        {
            var controller = new organizationController(new MockContext());

            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            expectedCode = System.Net.HttpStatusCode.Conflict;
            expectedStatus = "reserved";

            CreateOrganization(controller, "organization", expectedCode, expectedStatus);
            CreateOrganization(controller, "fleet", expectedCode, expectedStatus);
            CreateOrganization(controller, "vehicle", expectedCode, expectedStatus);
            CreateOrganization(controller, "location", expectedCode, expectedStatus);

            IEnumerable<Organization> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

        }
    }
}