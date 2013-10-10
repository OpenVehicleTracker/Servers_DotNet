using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnet.openvehicletracker.org.Tests.Controllers.apiTests
{
    public class BaseApiTest
    {
        public static void CreateRequest<T>(T controller) where T : System.Web.Http.ApiController
        {
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties.Add(System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new System.Web.Http.HttpConfiguration());
            controller.Request = request;
        }

    }
}
