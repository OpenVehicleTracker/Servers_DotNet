using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet.openvehicletracker.org.Controllers.api
{
    public class fleetController : BaseApiController
    {
        // GET /{organization}/fleet/{?name}
        public dynamic Get(string orgname, string name=null)
        {
            var organization = Entities.Organizations.Where(m => m.Name == name).FirstOrDefault();
            if (organization == null)
                return OrganizationNotFoundResponse();

            var fleets = string.IsNullOrEmpty(name) ? organization.Fleets : organization.Fleets.Where(m => m.Name == name);

            return fleets;

        }

        // POST /{organization}/fleet/{?name}
        public HttpResponseMessage Post(string orgname, [FromBody]dynamic value)
        {
            string name = value.name;
            if (string.IsNullOrWhiteSpace(name))
                return InvalidResponse();

            var organization = Entities.Organizations.Where(m => m.Name == orgname).FirstOrDefault();
            if (organization == null)
                return OrganizationNotFoundResponse();

            if (organization.Fleets.Any(m => m.Name == name))
                return ExistsResponse();

            try
            {
                Entities.Fleets.Add(new Models.Entities.Fleet() { Organization=organization, Name = name });
                Entities.SaveChanges();
                return CreatedResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }

        }

        //// PUT api/fleet/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/fleet/5
        //public void Delete(int id)
        //{
        //}
    }
}
