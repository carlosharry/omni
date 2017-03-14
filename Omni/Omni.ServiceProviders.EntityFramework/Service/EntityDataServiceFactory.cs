using Omni.Core.Service;
using Omni.ServiceProviders.EntityFramework.Factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Service
{
    public class EntityDataServiceFactory : IDataServiceFactory
    {
        public IDataService<T> Create<T>() where T : class
        {
            var context = ContextFactory.GetInstance().GetContext();
            return new EntityDataService<T>(context);
        }
    }
}
