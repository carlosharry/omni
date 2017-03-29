using Omni.Core.Infrastructure.Interfaces;
using Omni.ServiceProviders.EntityFramework.Infrastructure;
using Omni.ServiceProviders.EntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Factory
{
    internal class OmniEFManager
    {
        private static OmniEFManager instance;
        private static object theLock = new object();

        private OmniEFConfiguration config;
        private Func<DbContext> context;
        private IOmniManager omniManager;

        internal OmniEFManager(Func<DbContext> context, OmniEFConfiguration config)
        {
            this.context = context ?? throw new ArgumentNullException("The context provider cannot be null");
            this.config = config ?? throw new ArgumentNullException("The configuration for EF cannot be null");
        }

        public static OmniEFManager GetInstance()
        {
            lock (theLock)
            {
                if (instance == null)
                {
                    throw new Exception("ContextFactory is not yet setup.");
                }

                return instance;
            }
        }

        internal static OmniEFManager SetupInstance(Func<DbContext> context, OmniEFConfiguration config)
        {
            lock (theLock)
            {
                if (instance != null)
                {
                    throw new Exception("ContextFactory is already setup.");
                }

                instance = new OmniEFManager(context, config);

                return instance;
            }
        }

        internal void SetConfiguration(OmniEFConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException("The configuration object cannot be null");
        }

        internal OmniEFConfiguration GetConfiguration()
        {
            //TODO: Custom Exceptions

            return this.config ?? throw new Exception("An internal error occured with Omni. The EF manager does not have a configuration set.");
        }

        internal DbContext GetContext()
        {
            return this.context() ?? throw new Exception("An error occured with Omni. The DbContext does not exist.");
        }

        internal void SetOmniManager(IOmniManager manager)
        {
            this.omniManager = manager ?? throw new ArgumentNullException("The OmniManager cannot be null.");
        }

        internal IOmniManager GetOmniManager()
        {
            return this.omniManager ?? throw new Exception("An internal error occured with Omni. The Omni Manager does not exist yet.");
        }

        internal void SetupForIntegrationWithCore(IOmniManager manager)
        {
            this.SetOmniManager(manager ?? throw new ArgumentNullException("The OmniManager cannot be null."));

            var config = this.GetConfiguration();

            if (config.MapContextToController)
            {
                this.mapContextToController();
            }

        }

        private void mapContextToController()
        {
            var manager = this.GetOmniManager();

            var actualContext = this.GetContext();

            var types = DbContextHelper.GetDbSetTypesFromContext(actualContext);


            //TODO: Register the types with the OmniManager.

        }
    }
}
