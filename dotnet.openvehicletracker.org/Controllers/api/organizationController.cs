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
        // GET api/organization/{?name}
        public dynamic Get(string name = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                    return Entities.Organizations.Where(m => m.Name == name);
                else
                    return Entities.Organizations;
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

                Entities.Organizations.Add(new Models.Entities.Organization() { Name = name });
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
