using Omni.ServiceProviders.EntityFramework.Factory;
using Omni.ServiceProviders.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Infrastructure
{
    public class OmniEFBuilder
    {
        public OmniEFBuilder MapContextToController()
        {

            return this;
        }

        public OmniEFBuilder Configure(Action<OmniEFConfiguration> configureFunction)
        {
            //TODO: Make error classes
            var instance = OmniEFManager.GetInstance() ?? throw new Exception("An internal error occured with Omni. The EF manager was not found.");

            var config = instance?.GetConfiguration() ?? throw new Exception("The config option is null but is not supposed to be.");

            configureFunction(config);

            instance.SetConfiguration(config);

            return this;
        }
    }
}
