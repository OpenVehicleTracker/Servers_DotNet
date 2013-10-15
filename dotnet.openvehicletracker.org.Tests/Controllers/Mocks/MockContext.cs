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
            var fleets=new MockDbSet<Fleet>();
            fleets.UpdateComplexAssociations += fleets_UpdateComplexAssociations;
            Fleets = fleets;

            var locations = new MockDbSet<Location>();
            locations.UpdateComplexAssociations += locations_UpdateComplexAssociations;
            Locations = locations;

            var organizations = new MockDbSet<Organization>();
            organizations.UpdateComplexAssociations += organizations_UpdateComplexAssociations;
            Organizations = organizations;
           
            var vehicles = new MockDbSet<Vehicle>();
            vehicles.UpdateComplexAssociations += vehicles_UpdateComplexAssociations;
            Vehicles = vehicles;
            
            UserProfiles = new MockDbSet<UserProfile>();
            UsersInRole = new MockDbSet<webpages_UsersInRole>();
            webpages_Membership = new MockDbSet<webpages_Membership>();
            webpages_OAuthMembership = new MockDbSet<webpages_OAuthMembership>();
            webpages_Roles = new MockDbSet<webpages_Roles>();
        }

        void vehicles_UpdateComplexAssociations(Vehicle entity)
        {
            if (entity.Fleet != null)
                entity.Fleet.Vehicles.Add(entity);
        }

        void organizations_UpdateComplexAssociations(Organization entity)
        {
        }

        void locations_UpdateComplexAssociations(Location entity)
        {
            if (entity.Vehicle != null)
                entity.Vehicle.Locations.Add(entity);
        }

        void fleets_UpdateComplexAssociations(Fleet entity)
        {
            if (entity.Organization != null)
                entity.Organization.Fleets.Add(entity);
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
