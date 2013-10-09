using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTimeOffset time { get; set; }
        public int vehicleId { get { return Vehicle.Id; } }
        public double latitude { get; set; }
        public double longitude { get; set; }

        [JsonIgnore]
        [Required]
        public Vehicle Vehicle { get; set; }

    }
}