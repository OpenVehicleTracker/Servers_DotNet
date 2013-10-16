using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet.openvehicletracker.org.Models.Entities;
using System.Collections.Generic;
using dotnet.openvehicletracker.org.Controllers.api;
using System.Linq;

namespace dotnet.openvehicletracker.org.Tests.Controllers.apiTests
{
    [TestClass]
    public class fleetControllerTests : BaseApiTest
    {
        [TestMethod]
        public void FleetGetTest()
        {
            string testOrgName = "orgName";
            string testFleetName = "fleetName";

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);

            CreateOrganization(orgController, testOrgName);

            IEnumerable<Fleet> resultArray = fleetController.Get(testOrgName);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual(0, resultArray.Count());

            CreateFleet(fleetController, testOrgName, testFleetName);

            resultArray = fleetController.Get(testOrgName);
            Assert.IsNotNull(resultArray);
            Assert.IsInstanceOfType(resultArray, typeof(IEnumerable<Fleet>));
            Assert.AreEqual(1, resultArray.Count());

            string actualName = resultArray.First().Name;
            string expectedName = testFleetName;
            Assert.AreEqual(expectedName, actualName);

            Fleet result = fleetController.Get(testOrgName, testFleetName);
            actualName = result.Name;
            expectedName = testFleetName;
            Assert.AreEqual(expectedName, actualName);

            dynamic expecterror = fleetController.Get("none");
            Assert.IsInstanceOfType(expecterror, typeof(System.Net.Http.HttpResponseMessage));
            var actualCode = ((System.Net.Http.HttpResponseMessage)expecterror).StatusCode;
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, actualCode);

            result = fleetController.Get(testOrgName, "none");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FleetCreateTest()
        {
            string testOrgName = "TestOrgName";
            string testOrgName2 = "TestOrgName Two";

            string testFleetName = "TestFleetName";
            string testFleetName2 = "TestFleetName Too";

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);

            IEnumerable<Fleet> result;

            CreateOrganization(orgController, testOrgName);
            CreateOrganization(orgController, testOrgName2);

            CreateFleet(fleetController, testOrgName, testFleetName);
            CreateFleet(fleetController, testOrgName, testFleetName2);

            CreateFleet(fleetController, testOrgName2, testFleetName);

            result = fleetController.Get(testOrgName);
            Assert.AreEqual(2, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testFleetName));
            Assert.IsTrue(result.Any(m => m.Name == testFleetName2));

            result = fleetController.Get(testOrgName2);
            Assert.AreEqual(1, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testFleetName));
            Assert.IsFalse(result.Any(m => m.Name == testFleetName2));
        }

        [TestMethod]
        public void FleetCreateDuplicateNamesTest()
        {
            string orgName = "OrgName";
            string testFleetNameValid = "validFleetName";
            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);

            CreateOrganization(orgController, orgName);

            IEnumerable<Fleet> result = fleetController.Get(orgName);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            CreateFleet(fleetController, orgName, testFleetNameValid);

            expectedCode = System.Net.HttpStatusCode.Conflict;
            expectedStatus = "exists";
            CreateFleet(fleetController, orgName, testFleetNameValid, expectedCode, expectedStatus);

            result = fleetController.Get(orgName);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            string actualName = result.FirstOrDefault().Name;
            string expectedName = testFleetNameValid;
            Assert.AreEqual(expectedName, actualName);

        }

    }
}
