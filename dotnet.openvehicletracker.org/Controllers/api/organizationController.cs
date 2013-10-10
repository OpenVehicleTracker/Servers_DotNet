using dotnet.openvehicletracker.org.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet.openvehicletracker.org.Controllers.api
{
    public class organizationController : BaseApiController
    {
        public organizationController() : this(null) { }
        public organizationController(IOVTContext entities = null) : base(entities) { }

        // GET api/organization/{?name}
        public dynamic Get(string name = null)
        {
            try
            {
                return Entities.Organizations.GetItemByNameOrList(name);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        // POST api/organization
        public HttpResponseMessage Post([FromBody]dynamic value)
        {
            try
            {
                string name = value.name;

                if (string.IsNullOrWhiteSpace(name))
                    return InvalidResponse();

                if (Entities.Organizations.Any(m => m.Name == name))
                    return ExistsResponse();

                var organization = new Models.Entities.Organization() { Name = name };
                if (!organization.NameIsAllowed(name))
                    return ReservedNameResponse();

                Entities.Organizations.Add(organization);
                Entities.SaveChanges();
                return CreatedResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        //public void Put(string name)
        //{
        //}

        //public void Delete(string name)
        //{
        //}
    }
}
