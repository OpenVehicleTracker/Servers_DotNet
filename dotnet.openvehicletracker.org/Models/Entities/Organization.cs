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
    [Table("Organizations")]
    public class Organization : BaseNamedEntity
    {
        [JsonIgnore]
        public virtual ICollection<Fleet> Fleets { get; set; }
    }
}
