using dotnet.openvehicletracker.org.Controllers.api;
using dotnet.openvehicletracker.org.Models.Entities;
using dotnet.openvehicletracker.org.Tests.Controllers.apiTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using System.Linq;
using System;

namespace dotnet.openvehicletracker.org.Tests.Controllers
{
    [TestClass]
    public class organizationControllerTests : BaseApiTest
    {

        [TestMethod]
        public void OrganizationCreateGetTest()
        {
            string testOrgName = "TestOrgName";
            string testOrgName2 = "TestOrgName Too";

            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            var controller = new organizationController(new MockContext());

            IDbSet<Organization> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            CreateOrganization(controller, testOrgName);

            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            string actualName = result.First().Name;
            string expectedName = testOrgName;
            Assert.AreEqual(expectedName, actualName);

            CreateOrganization(controller, testOrgName2);
            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testOrgName));
            Assert.IsTrue(result.Any(m => m.Name == testOrgName2));
        }

        [TestMethod]
        public void OrganizationCreateDuplicateNamesTest()
        {
            string testOrgNameValid = "TestOrgName";
            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            var controller = new organizationController(new MockContext());

            IDbSet<Organization> result = controller.Get();
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

            IDbSet<Organization> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

        }
    }
}