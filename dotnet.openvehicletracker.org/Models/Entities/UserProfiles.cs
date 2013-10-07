using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        public UserProfile()
        {
            this.Organizations = new List<Organization>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, StringLength(56)]
        public string UserName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [ForeignKey("UserId")]
        public virtual webpages_Membership Membership { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }


}