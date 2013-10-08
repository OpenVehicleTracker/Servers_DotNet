using dotnet.openvehicletracker.org.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet.openvehicletracker.org.Controllers.api
{
    public class BaseApiController : ApiController
    {

        protected class StatusResponse
        {
            public static readonly StatusResponse Created = new StatusResponse() { status = "created", code= HttpStatusCode.Created };
            public static readonly StatusResponse Exists = new StatusResponse() { status = "exists" , code= HttpStatusCode.Conflict };
            public static readonly StatusResponse Invalid = new StatusResponse() { status = "invalid", code= HttpStatusCode.Conflict  };
            public static readonly StatusResponse Error = new StatusResponse() { status = "error", code = HttpStatusCode.InternalServerError };
            public static readonly StatusResponse OrganizationNotFound = new StatusResponse() { status = "organization", code = HttpStatusCode.NotFound };

            public string status { get; set; }
            public HttpStatusCode code { get; set; }
        }

        protected HttpResponseMessage CreatedResponse() { return CreateResponse(StatusResponse.Created); }
        protected HttpResponseMessage InvalidResponse() { return CreateResponse(StatusResponse.Invalid); }
        protected HttpResponseMessage ExistsResponse() { return CreateResponse(StatusResponse.Exists); }
        protected HttpResponseMessage ErrorResponse(Exception ignored = null) { return CreateResponse(StatusResponse.Error); }
        protected HttpResponseMessage OrganizationNotFoundResponse(Exception ignored = null) { return CreateResponse(StatusResponse.OrganizationNotFound); }

        protected HttpResponseMessage CreateResponse(StatusResponse status)
        {
            return Request.CreateResponse(status.code, status.status);
        }

        public static readonly OVTContext Entities = new OVTContext();
    }
}
