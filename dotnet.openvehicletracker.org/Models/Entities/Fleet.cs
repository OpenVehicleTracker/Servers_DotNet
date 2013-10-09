using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    [Table("Fleets")]
    public class Fleet : BaseNamedEntity
    {
        [JsonIgnore]
        [Required]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vehicle> Vehicles { get; set; }

    }
}
