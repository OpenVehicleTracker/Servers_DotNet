﻿using System;
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
        public dynamic Get(string orgname, string name = null)
        {
            try
            {
                var organization = Entities.Organizations.Where(m => m.Name == orgname).FirstOrDefault();
                if (organization == null)
                    return OrganizationNotFoundResponse();

                if (!string.IsNullOrEmpty(name))
                    return organization.Fleets.Where(m => m.Name == name).FirstOrDefault();
                else
                    return organization.Fleets;
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

                var organization = GetOrganizationByName(orgname);

                if (organization.Fleets.Any(m => m.Name == name))
                    return ExistsResponse();

                Entities.Fleets.Add(new Models.Entities.Fleet() { Organization = organization, Name = name });
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
