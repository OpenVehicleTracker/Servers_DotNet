using dotnet.openvehicletracker.org.Models.Entities;
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
        public fleetController() : this(null) { }
        public fleetController(IOVTContext entities = null) : base(entities) { }

        // GET /{organization}/fleet/{?name}
        public dynamic Get(string orgname, string name = null)
        {
            try
            {
                Organization organization;
                GetOrganizationEntities(orgname, out organization);
                return organization.Fleets.GetItemByNameOrList(name);
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

        // POST /{organization}/fleet
        public HttpResponseMessage Post(string orgname, [FromBody]dynamic value)
        {
            try
            {
                string name = value.name;
                if (string.IsNullOrWhiteSpace(name))
                    return InvalidResponse();

                Organization organization;
                GetOrganizationEntities(orgname, out organization);

                if (organization.Fleets.Any(m => m.Name == name))
                    return ExistsResponse();

                if (!organization.NameIsAllowed(name))
                    return ReservedNameResponse();

                Entities.Fleets.Add(new Models.Entities.Fleet() { Organization = organization, Name = name });
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
