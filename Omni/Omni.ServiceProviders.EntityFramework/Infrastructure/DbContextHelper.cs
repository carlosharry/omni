using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Infrastructure
{
    public static class DbContextHelper
    {
        public static IEnumerable<Type> GetDbSetTypesFromContext(DbContext context)
        {
            var tType = context.GetType();

            //var theProps = tType.GetProperties()?.Where(x => typeof(DbSet<>).IsAssignableFrom(x.PropertyType)).ToList();

            var theProps = tType.GetProperties()?.Where(x => x.PropertyType.IsGenericType && (x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))).ToList();

            foreach (var dbProps in theProps)
            {

                var type = dbProps.PropertyType.GetGenericArguments().FirstOrDefault();

                if (type == null)
                {
                    continue;
                }

                yield return type;
            }
        }
    }
}
