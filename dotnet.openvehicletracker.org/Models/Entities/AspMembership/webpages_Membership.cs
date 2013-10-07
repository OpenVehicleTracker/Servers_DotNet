using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public partial class webpages_Membership
    {
        public webpages_Membership()
        {
            OAuthMemberships = new List<webpages_OAuthMembership>();
            UsersInRoles = new List<webpages_UsersInRole>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        [StringLength(128)]
        public string ConfirmationToken { get; set; }

        public Nullable<bool> IsConfirmed { get; set; }

        public Nullable<System.DateTime> LastPasswordFailureDate { get; set; }
        
        public int PasswordFailuresSinceLastSuccess { get; set; }
        
        [Required, StringLength(128)]
        public string Password { get; set; }
        
        public Nullable<System.DateTime> PasswordChangedDate { get; set; }
        
        [Required, StringLength(128)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }

        public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<webpages_OAuthMembership> OAuthMemberships { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<webpages_UsersInRole> UsersInRoles { get; set; }
    }
}
