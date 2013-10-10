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
        private static readonly IOVTContext _Entities = new OVTContext();
        protected static IOVTContext Entities { get; private set; }

        public BaseOVTController(IOVTContext entities = null)
        {
            Entities = entities ?? _Entities;
        }
    }
}
