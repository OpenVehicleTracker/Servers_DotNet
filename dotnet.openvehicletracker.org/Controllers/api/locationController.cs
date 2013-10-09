using dotnet.openvehicletracker.org.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet.openvehicletracker.org.Controllers.api
{
    public class locationController : BaseApiController
    {
        // GET /{organization}/{fleet}/{vehicle}/location/{?start}
        public dynamic Get(string orgname, string fleetname, string vehiclename, int? start = null)
        {
            try
            {
                Organization organization;
                Fleet fleet;
                Vehicle vehicle;

                GetOrganizationEntities(orgname, fleetname, vehiclename, out organization, out fleet, out vehicle);

                int startid = start.HasValue ? start.Value : 0;
                if (startid > 0)
                    return vehicle.Locations.Where(m => m.Id > startid);
                else
                    return vehicle.Locations;
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

        // POST api/{orgname}/{fleetname}/vehicle/location
        public HttpResponseMessage Post(string orgname, string fleetname, [FromBody]dynamic value)
        {
            try
            {
                Organization organization;
                Fleet fleet;

                GetOrganizationEntities(orgname, fleetname, out organization, out fleet);

                DateTimeOffset time = DateTimeOffset.Parse(value.time.ToString(), null, DateTimeStyles.RoundtripKind); // ISO8601 
                int vehicleId = value.vehicleId;
                double latitude = value.latitude;
                double longitude = value.longitude;

                Vehicle vehicle=fleet.Vehicles.Where(m=>m.Id==vehicleId).FirstOrDefault();
                if(vehicle==null) throw new VehicleNotFoundException();

                Location location = new Location() { time = time, Vehicle = vehicle, latitude = latitude, longitude = longitude };
                vehicle.Locations.Add(location);
                Entities.SaveChanges();

                return LoggedResponse();

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

        //// PUT api/location/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/location/5
        //public void Delete(int id)
        //{
        //}
    }
}
