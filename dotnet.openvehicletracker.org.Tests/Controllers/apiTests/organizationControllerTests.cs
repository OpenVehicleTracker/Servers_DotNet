using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet.openvehicletracker.org.Controllers.api;
using System.Linq;
using System.Collections;
using dotnet.openvehicletracker.org.Models.Entities;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using dotnet.openvehicletracker.org.Tests.Controllers.apiTests;

namespace dotnet.openvehicletracker.org.Tests.Controllers
{
    [TestClass]
    public class organizationControllerTests : BaseApiTest
    {
        
        [TestMethod]
        public void CreateOrganizationTest()
        {
            var controller = new organizationController(new MockContext());

            IDbSet<Organization> result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());

            CreateRequest(controller);

            var response = controller.Post(JObject.Parse("{'name':'TestOrgName'}"));
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);

            result = controller.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

    }
}
