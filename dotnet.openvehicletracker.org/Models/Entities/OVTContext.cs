using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public class OVTContext : DbContext, IOVTContext
    {

        public OVTContext()
            : base("Name=DefaultConnection")
        {
        }

        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<Fleet> Fleets { get; set; }
        public IDbSet<Vehicle> Vehicles { get; set; }
        public IDbSet<Location> Locations { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<webpages_Membership> webpages_Membership { get; set; }
        public IDbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public IDbSet<webpages_Roles> webpages_Roles { get; set; }
        public IDbSet<webpages_UsersInRole> UsersInRole { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}