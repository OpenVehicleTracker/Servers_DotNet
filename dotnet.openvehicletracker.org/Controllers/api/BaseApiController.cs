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
            public static readonly StatusResponse Created = new StatusResponse() { status = "created", code = HttpStatusCode.Created };
            public static readonly StatusResponse Logged = new StatusResponse() { status = "logged", code = HttpStatusCode.Created };
            public static readonly StatusResponse Exists = new StatusResponse() { status = "exists", code = HttpStatusCode.Conflict };
            public static readonly StatusResponse Reserved = new StatusResponse() { status = "reserved", code = HttpStatusCode.Conflict };
            public static readonly StatusResponse Invalid = new StatusResponse() { status = "invalid", code = HttpStatusCode.Conflict };
            public static readonly StatusResponse Error = new StatusResponse() { status = "error", code = HttpStatusCode.InternalServerError };
            public static readonly StatusResponse NotFound = new StatusResponse() { status = "unknown", code = HttpStatusCode.NotFound };

            public string status { get; set; }
            public HttpStatusCode code { get; set; }
        }

        protected HttpResponseMessage CreatedResponse() { return CreateResponse(StatusResponse.Created); }
        protected HttpResponseMessage LoggedResponse() { return CreateResponse(StatusResponse.Logged); }
        protected HttpResponseMessage InvalidResponse() { return CreateResponse(StatusResponse.Invalid); }
        protected HttpResponseMessage ExistsResponse() { return CreateResponse(StatusResponse.Exists); }
        protected HttpResponseMessage ReservedNameResponse() { return CreateResponse(StatusResponse.Reserved); }
        protected HttpResponseMessage ErrorResponse(Exception ignored = null) { return CreateResponse(StatusResponse.Error); }

        protected HttpResponseMessage NotFoundResponse(EntityNotFoundException ex) { return CreateResponse(StatusResponse.NotFound, ex.EntityTypeName); }

        protected HttpResponseMessage CreateResponse(StatusResponse response, string statusvalue = null)
        {
            return Request.CreateResponse(response.code, new { status = (statusvalue ?? response.status) });
        }

        public static readonly OVTContext Entities = new OVTContext();

        protected static void GetOrganizationEntities(string orgname, out Models.Entities.Organization organization)
        {
            organization = GetOrganizationByName(orgname, true);
        }

        protected static void GetOrganizationEntities(string orgname, string fleetname, out Models.Entities.Organization organization, out Models.Entities.Fleet fleet)
        {
            GetOrganizationEntities(orgname, out organization);
            fleet = organization == null ? null : GetFleetByName(organization, fleetname, true);
        }

        protected static void GetOrganizationEntities(string orgname, string fleetname, string vehiclename, out Models.Entities.Organization organization, out Models.Entities.Fleet fleet, out Models.Entities.Vehicle vehicle)
        {
            GetOrganizationEntities(orgname, fleetname, out organization, out fleet);
            vehicle = fleet == null ? null : GetVehicleByName(fleet, vehiclename, true);
        }

        protected static Organization GetOrganizationByName(string orgname, bool ThrowIfNotFound = false)
        {
            var organization = Entities.Organizations.Where(m => m.Name == orgname).FirstOrDefault();
            if (ThrowIfNotFound && organization == null) throw new OrganizationNotFoundException();
            return organization;
        }

        protected static Fleet GetFleetByName(Organization organization, string fleetname, bool ThrowIfNotFound = false)
        {
            if (ThrowIfNotFound && organization == null) throw new OrganizationNotFoundException();
            var fleet = organization.Fleets.Where(m => m.Name == fleetname).FirstOrDefault();
            if (ThrowIfNotFound && fleet == null) throw new FleetNotFoundException();
            return fleet;
        }

        protected static Vehicle GetVehicleByName(Fleet fleet, string vehiclename, bool ThrowIfNotFound = false)
        {
            if (ThrowIfNotFound && fleet == null) throw new FleetNotFoundException();
            var vehicle = fleet.Vehicles.Where(m => m.Name == vehiclename).FirstOrDefault();
            if (ThrowIfNotFound && vehicle == null) throw new VehicleNotFoundException();
            return vehicle;
        }


        protected class OrganizationNotFoundException : EntityNotFoundException
        {
            public OrganizationNotFoundException() : base("organization") { }
        }
      
        protected class FleetNotFoundException : EntityNotFoundException
        {
            public FleetNotFoundException() : base("fleet") { }
        }

        protected class VehicleNotFoundException : EntityNotFoundException
        {
            public VehicleNotFoundException() : base("vehicle") { }
        }

        protected class EntityNotFoundException : Exception
        {
            public string EntityTypeName { get; private set; }
            public EntityNotFoundException(string entitytypename)
            {
                EntityTypeName = entitytypename;
            }
        }


    }
}
