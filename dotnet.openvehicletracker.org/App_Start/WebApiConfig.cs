using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace dotnet.openvehicletracker.org
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "OrganizationApi",
                routeTemplate: "api/organization/{name}",
                defaults: new { controller = "organization", name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FleetApi",
                routeTemplate: "api/{orgname}/fleet/{name}",
                defaults: new { controller = "fleet", name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VehicleApi",
                routeTemplate: "api/{orgname}/{fleetname}/vehicle/{name}",
                defaults: new { controller = "vehicle", name = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "LocationApi",
               routeTemplate: "api/{orgname}/{fleetname}/{vehiclename}/location/{start},{end}",
               defaults: new { controller = "location", start = RouteParameter.Optional, end = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}