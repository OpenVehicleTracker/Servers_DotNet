using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;

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
                defaults: new { controller = "vehicle", name = RouteParameter.Optional },
                constraints: new { name = "^(?!location)" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "LocationApiPOST",
               routeTemplate: "api/{orgname}/{fleetname}/vehicle/location",
               defaults: new { controller = "location" },
               constraints: new { httpMethod = new HttpMethodConstraint("POST") }
           );

            config.Routes.MapHttpRoute(
               name: "LocationApiGET",
               routeTemplate: "api/{orgname}/{fleetname}/{vehiclename}/location/{start}",
               defaults: new { controller = "location", start = RouteParameter.Optional },
               constraints: new { httpMethod = new HttpMethodConstraint("GET") }
           );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
        }
    }
}