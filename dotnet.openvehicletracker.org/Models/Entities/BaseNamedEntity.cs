using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public class BaseNamedEntity : INamedEntity
    {
        private static readonly string[] reservedNames = { "organization", "fleet", "vehicle", "location" };

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(80)]
        public string Name { get; set; }

        public virtual bool NameIsAllowed(string name)
        {
            return !reservedNames.Contains(name);
        }

    }
}