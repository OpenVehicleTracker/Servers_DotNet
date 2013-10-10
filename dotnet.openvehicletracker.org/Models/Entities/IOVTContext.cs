using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
namespace dotnet.openvehicletracker.org.Models.Entities
{
    public interface IOVTContext
    {
        IDbSet<Fleet> Fleets { get; set; }
        IDbSet<Location> Locations { get; set; }
        IDbSet<Organization> Organizations { get; set; }
        IDbSet<Vehicle> Vehicles { get; set; }

        IDbSet<UserProfile> UserProfiles { get; set; }
        IDbSet<webpages_UsersInRole> UsersInRole { get; set; }
        IDbSet<webpages_Membership> webpages_Membership { get; set; }
        IDbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        IDbSet<webpages_Roles> webpages_Roles { get; set; }

        int SaveChanges();
    }
}
