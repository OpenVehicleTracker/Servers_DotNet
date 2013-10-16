using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet.openvehicletracker.org.Controllers.api;
using System.Collections.Generic;
using dotnet.openvehicletracker.org.Models.Entities;
using System.Linq;

namespace dotnet.openvehicletracker.org.Tests.Controllers.apiTests
{
    [TestClass]
    public class vehicleControllerTests : BaseApiTest
    {
        [TestMethod]
        public void VehicleGetTest()
        {
            string testOrgName = "orgName";
            string testFleetName = "fleetName";
            string testVehicleName = "vehicleName";

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);
            var vehicleController = new vehicleController(context);

            CreateOrganization(orgController, testOrgName);
            CreateFleet(fleetController, testOrgName, testFleetName);

            IEnumerable<Vehicle> resultArray = vehicleController.Get(testOrgName, testFleetName);
            Assert.IsNotNull(resultArray);
            Assert.AreEqual(0, resultArray.Count());

            CreateVehicle(vehicleController, testOrgName, testFleetName, testVehicleName);

            resultArray = vehicleController.Get(testOrgName, testFleetName);
            Assert.IsNotNull(resultArray);
            Assert.IsInstanceOfType(resultArray, typeof(IEnumerable<Vehicle>));
            Assert.AreEqual(1, resultArray.Count());

            string actualName = resultArray.First().Name;
            string expectedName = testVehicleName;
            Assert.AreEqual(expectedName, actualName);

            Vehicle result = vehicleController.Get(testOrgName, testFleetName, testVehicleName);
            actualName = result.Name;
            expectedName = testVehicleName;
            Assert.AreEqual(expectedName, actualName);

            dynamic expecterror = vehicleController.Get(testOrgName, "none");
            Assert.IsInstanceOfType(expecterror, typeof(System.Net.Http.HttpResponseMessage));
            var actualCode = ((System.Net.Http.HttpResponseMessage)expecterror).StatusCode;
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, actualCode);

            result = vehicleController.Get(testOrgName, testFleetName, "none");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void VehicleCreateTest()
        {
            string testOrgName = "TestOrgName";

            string testFleetName = "TestFleetName";
            string testFleetName2 = "TestFleetName Too";

            string testVehicleName = "Snowplow";
            string testVehicleName2 = "Schoolbus";

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);
            var vehicleController = new vehicleController(context);

            IEnumerable<Vehicle> result;

            CreateOrganization(orgController, testOrgName);

            CreateFleet(fleetController, testOrgName, testFleetName);
            CreateFleet(fleetController, testOrgName, testFleetName2);

            CreateVehicle(vehicleController, testOrgName, testFleetName, testVehicleName);
            CreateVehicle(vehicleController, testOrgName, testFleetName2, testVehicleName2);
            CreateVehicle(vehicleController, testOrgName, testFleetName2, testVehicleName);

            result = vehicleController.Get(testOrgName, testFleetName);
            Assert.AreEqual(1, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testVehicleName));
            Assert.IsFalse(result.Any(m => m.Name == testVehicleName2));

            result = vehicleController.Get(testOrgName, testFleetName2);
            Assert.AreEqual(2, result.Count());

            Assert.IsTrue(result.Any(m => m.Name == testVehicleName));
            Assert.IsTrue(result.Any(m => m.Name == testVehicleName2));
        }

        [TestMethod]
        public void VehicleCreateDuplicateNamesTest()
        {
            string orgName = "OrgName";
            string fleetName = "FleetName";
            string testVehicleName = "Snowplow";

            System.Net.HttpStatusCode expectedCode;
            string expectedStatus;

            var context = new MockContext();

            var orgController = new organizationController(context);
            var fleetController = new fleetController(context);
            var vehicleController = new vehicleController(context);

            CreateOrganization(orgController, orgName);
            CreateFleet(fleetController, orgName, fleetName);
            CreateVehicle(vehicleController, orgName, fleetName, testVehicleName);

            expectedCode = System.Net.HttpStatusCode.Conflict;
            expectedStatus = "exists";
            CreateVehicle(vehicleController, orgName, fleetName, testVehicleName, expectedCode, expectedStatus);

            IEnumerable<Vehicle> result = vehicleController.Get(orgName, fleetName);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            string actualName = result.FirstOrDefault().Name;
            string expectedName = testVehicleName;
            Assert.AreEqual(expectedName, actualName);

        }

    }
}
