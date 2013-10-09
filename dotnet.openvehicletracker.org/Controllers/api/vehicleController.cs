using dotnet.openvehicletracker.org.Models.Entities;
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
                Organization organization;
                Fleet fleet;
                GetOrganizationEntities(orgname, fleetname, out organization, out fleet);

                return fleet.Vehicles.GetItemByNameOrList(name);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFoundResponse(ex);
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

                Organization organization;
                Fleet fleet;
                GetOrganizationEntities(orgname, fleetname, out organization, out fleet);

                if (fleet.Vehicles.Any(m => m.Name == name))
                    return ExistsResponse();

                if(!fleet.NameIsAllowed(name))
                    return ReservedNameResponse();

                fleet.Vehicles.Add(new Models.Entities.Vehicle() { Fleet = fleet, Name = name });
                Entities.SaveChanges();
                return CreatedResponse();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFoundResponse(ex);
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
