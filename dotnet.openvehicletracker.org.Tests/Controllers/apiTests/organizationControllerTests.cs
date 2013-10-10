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
        public void OrganizationGetCreateTest()
        {
            string testOrgNameValid = "TestOrgName";
            string testOrgNameValid2 = "TestOrgName Too";
            string testOrgNameReserved = "organization";

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

            expectedCode = System.Net.HttpStatusCode.Conflict;
            expectedStatus = "reserved";
            CreateOrganization(controller, testOrgNameReserved, expectedCode, expectedStatus);

            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            string actualName = result.FirstOrDefault().Name;
            string expectedName = testOrgNameValid;
            Assert.AreEqual(expectedName, actualName);

            CreateOrganization(controller, testOrgNameValid2);
            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testOrgNameValid));
            Assert.IsTrue(result.Any(m => m.Name == testOrgNameValid2));
        }

    }
}
