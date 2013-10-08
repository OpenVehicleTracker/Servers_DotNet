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
        // GET api/organization
        public IEnumerable<object> Get(string name = null)
        {
            var organizations = string.IsNullOrEmpty(name) ? Entities.Organizations : Entities.Organizations.Where(m => m.Name == name);
            
            return organizations.Select(m => new { id = m.Id, name = m.Name });
        }

        // POST api/organization
        public HttpResponseMessage Post([FromBody]dynamic value)
        {
            string name = value.name;

            if (string.IsNullOrWhiteSpace(name))
                return InvalidResponse();

            if (Entities.Organizations.Any(m => m.Name == name))
                return ExistsResponse();

            try
            {
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
