using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public partial class webpages_Roles
    {
        public webpages_Roles()
        {
            this.UsersInRoles = new List<webpages_UsersInRole>();
        }

        [Key]
        public int RoleId { get; set; }
        
        [StringLength(256)]
        public string RoleName { get; set; }

        [ForeignKey("RoleId")]
        public virtual ICollection<webpages_UsersInRole> UsersInRoles { get; set; }
    }

    [Table("webpages_UsersInRoles")]
    public partial class webpages_UsersInRole
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [Column("RoleId"), InverseProperty("UsersInRoles")]
        public virtual webpages_Roles Roles { get; set; }

        [Column("UserId"), InverseProperty("UsersInRoles")]
        public virtual webpages_Membership Members { get; set; }

    }

}
