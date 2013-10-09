using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    [Table("Vehicles")]
    public class Vehicle : BaseNamedEntity
    {
        [JsonIgnore]
        [Required]
        public virtual Fleet Fleet { get; set; }
   
        [JsonIgnore]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
