using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Factory
{
    public class ContextFactory
    {
        private static ContextFactory instance;
        private static object theLock = new object();


        private Func<DbContext> context;

        internal ContextFactory(Func<DbContext> context)
        {
            this.context = context ?? throw new ArgumentNullException("The context provider cannot be null");
        }

        public static ContextFactory GetInstance()
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

        internal static ContextFactory SetupInstance(Func<DbContext> context)
        {
            lock (theLock)
            {
                if (instance != null)
                {
                    throw new Exception("ContextFactory is already setup.");
                }

                instance = new ContextFactory(context);

                return instance;
            }
        }

        public DbContext GetContext()
        {
            return this.context();
        }
    }
}
