using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet.openvehicletracker.org.Controllers.api
{
    public class vehicleController : BaseApiController
    {
        // GET api/{orgname}/{fleetname}/vehicle/{?name}
        public dynamic Get(string orgname, string fleetname, string name = null)
        {
            try
            {
                var organization = GetOrganizationByName(orgname);
                if (organization == null)
                    return OrganizationNotFoundResponse();

                var fleet = GetFleetByName(organization, fleetname);
                if (fleet == null)
                    return FleetNotFoundResponse();

                if (!string.IsNullOrEmpty(name))
                    return fleet.Vehicles.Where(m => m.Name == name);
                else
                    return fleet.Vehicles;
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }

        }

        // POST api/{orgname}/{fleetname}/vehicle
        public HttpResponseMessage Post(string orgname, string fleetname, [FromBody]dynamic value)
        {
            try
            {
                string name = value.name;
                if (string.IsNullOrWhiteSpace(name))
                    return InvalidResponse();

                var organization = GetOrganizationByName(orgname);
                if (organization == null)
                    return OrganizationNotFoundResponse();

                var fleet = GetFleetByName(organization, fleetname);
                if (fleet == null)
                    return FleetNotFoundResponse();

                if (fleet.Vehicles.Any(m => m.Name == name))
                    return ExistsResponse();

                fleet.Vehicles.Add(new Models.Entities.Vehicle() { Fleet = fleet, Name = name });
                Entities.SaveChanges();
                return CreatedResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        //// PUT api/vehicle/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/vehicle/5
        //public void Delete(int id)
        //{
        //}
    }
}
