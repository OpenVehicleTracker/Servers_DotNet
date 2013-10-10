using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnet.openvehicletracker.org.Models.Entities;
using System.Data.Entity;

namespace dotnet.openvehicletracker.org.Tests.Controllers
{
    class MockContext : IOVTContext
    {

        public MockContext()
        {
            Fleets = new MockDbSet<Fleet>();
            Locations = new MockDbSet<Location>();
            Organizations = new MockDbSet<Organization>();
            Vehicles = new MockDbSet<Vehicle>();
            UserProfiles = new MockDbSet<UserProfile>();
            UsersInRole = new MockDbSet<webpages_UsersInRole>();
            webpages_Membership = new MockDbSet<webpages_Membership>();
            webpages_OAuthMembership = new MockDbSet<webpages_OAuthMembership>();
            webpages_Roles = new MockDbSet<webpages_Roles>();
        }

        public IDbSet<Fleet> Fleets { get; set; }

        public IDbSet<Location> Locations { get; set; }

        public IDbSet<Organization> Organizations { get; set; }

        public IDbSet<Vehicle> Vehicles { get; set; }

        public IDbSet<UserProfile> UserProfiles { get; set; }

        public IDbSet<webpages_UsersInRole> UsersInRole { get; set; }

        public IDbSet<webpages_Membership> webpages_Membership { get; set; }

        public IDbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }

        public IDbSet<webpages_Roles> webpages_Roles { get; set; }

        public int SaveChanges()
        {
            int toReturn = 0;
            toReturn += SaveChanges(Fleets);
            toReturn += SaveChanges(Locations);
            toReturn += SaveChanges(Organizations);
            toReturn += SaveChanges(Vehicles);
            toReturn += SaveChanges(UserProfiles);
            toReturn += SaveChanges(UsersInRole);
            toReturn += SaveChanges(webpages_Membership);
            toReturn += SaveChanges(webpages_OAuthMembership);
            toReturn += SaveChanges(webpages_Roles);

            return toReturn;
        }

        public enum state
        {
            sync,
            add,
            delete
        }

        private int SaveChanges<T>(IDbSet<T> iset) where T : class, new()
        {
            MockDbSet<T> set = iset as MockDbSet<T>;
            
            if(set==null) return 0;

            int toReturn = 0;

            var toRemove = set.repo.Where(m => m.Value == state.delete).Select(m => m.Key).ToList();
            foreach (var item in toRemove)
            {
                set.repo.Remove(item);
                toReturn++;
            }

            var toSync = set.repo.Where(m => m.Value == state.add).Select(m => m.Key).ToList();
            foreach (var item in toSync)
            {
                set.repo[item] = state.sync;
                toReturn++;
            }

            return toReturn;
        }


    }
}
