using dotnet.openvehicletracker.org.Controllers.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dotnet.openvehicletracker.org.Tests.Controllers.apiTests
{
    public class BaseApiTest
    {
        public static System.Net.Http.HttpRequestMessage CreateRequest()
        {
            var request = new System.Net.Http.HttpRequestMessage();
            request.Properties.Add(System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new System.Web.Http.HttpConfiguration());
            return request;
        }

        public void CreateOrganization(organizationController controller, string orgName, System.Net.HttpStatusCode expectedCode = System.Net.HttpStatusCode.Created, string expectedStatus = "created")
        {
            string postJson = "{'name':'" + orgName + "'}";

            System.Net.HttpStatusCode actualCode;
            dynamic actualStatus;

            PostJsonGetResults(controller, postJson, out actualCode, out actualStatus);
            Assert.AreEqual(expectedCode, actualCode);
            Assert.AreEqual(expectedStatus, actualStatus);
        }

        public void PostJsonGetResults<T>(T controller, string postJson, out System.Net.HttpStatusCode actualCode, out dynamic actualStatus) where T : BaseApiController
        {
            PostJsonGetResults(controller, new object[] { JObject.Parse(postJson) }, out actualCode, out actualStatus);
        }

        public void PostJsonGetResults<T>(T controller, object[] parameters, out System.Net.HttpStatusCode actualCode, out dynamic actualStatus) where T : BaseApiController
        {
            controller.Request = CreateRequest();

            var post = typeof(T).GetMethod("Post");
            dynamic response = post.Invoke(controller, System.Reflection.BindingFlags.InvokeMethod, null, parameters, System.Globalization.CultureInfo.CurrentCulture);
            
            dynamic message = ParseJsonResponse(response);
            actualCode = response.StatusCode;
            actualStatus = JsonValueAsString(message.status);
        }

        public static dynamic ParseJsonResponse(System.Net.Http.HttpResponseMessage response)
        {
            string stringcontent;
            using (MemoryStream ms = new MemoryStream())
            {
                response.Content.CopyToAsync(ms);
                stringcontent = System.Text.UTF8Encoding.UTF8.GetString(ms.ToArray());
            }

            dynamic message = JObject.Parse(stringcontent);
            return message;
        }

        public static string JsonValueAsString(dynamic jvalue)
        {
            return ((JValue)jvalue).ToString();
        }


    }
}
