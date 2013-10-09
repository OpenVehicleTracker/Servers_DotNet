using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace dotnet.openvehicletracker.org.Models.Entities
{
    public static class EntityExtensions
    {
        public static dynamic GetItemByNameOrList<T>(this IEnumerable<T> source, string name) where T : BaseNamedEntity
        {
            if (!string.IsNullOrEmpty(name))
                return source.Where(m => m.Name == name).FirstOrDefault();
            else
                return source;
        }
    }
}