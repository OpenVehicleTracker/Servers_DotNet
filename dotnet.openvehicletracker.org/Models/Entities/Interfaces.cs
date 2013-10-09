using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public interface INamedEntity
    {
        string Name { get; set; }
    }
}