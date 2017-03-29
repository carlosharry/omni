using Omni.Core.Service;
using Omni.ServiceProviders.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Service
{
    public class EntityDataService : IDataService
    {
        public DbContext Context { get; private set; }

        public EntityDataService(DbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException("The DbContext cannot be null");
        }

        public IDataServiceRepository<T> Repository<T>() where T : class
        {
            return new EntityDataServiceRepository<T>(this.Context);
        }


        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

       
    }
}
