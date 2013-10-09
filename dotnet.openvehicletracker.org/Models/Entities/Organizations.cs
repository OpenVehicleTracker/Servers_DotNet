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
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Fleet> Fleets { get; set; }
    }
}
