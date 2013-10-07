using dotnet.openvehicletracker.org.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dotnet.openvehicletracker.org.Controllers
{
    public class BaseOVTController : Controller
    {
        public static readonly OVTContext Entities = new OVTContext();
    }
}
