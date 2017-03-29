using Omni.Core.Service;
using Omni.ServiceProviders.EntityFramework.Factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omni.Core.Infrastructure.Interfaces;

namespace Omni.ServiceProviders.EntityFramework.Service
{
    public class EntityDataServiceProvider : IDataServiceProvider
    {
        public IDataService Create()
        {
            var context = OmniEFManager.GetInstance().GetContext();
            return new EntityDataService(context);
        }

        public void Setup(IOmniManager manager)
        {
            OmniEFManager.GetInstance().SetupForIntegrationWithCore(manager);
        }
    }
}
