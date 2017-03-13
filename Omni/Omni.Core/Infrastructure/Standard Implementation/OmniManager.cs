using Omni.Core.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Infrastructure.Standard_Implementation
{
    public class OmniManager : IOmniManager
    {
        private static OmniManager instance;
        private static object theLock = new object();


        private IServiceProvider provider;

        internal OmniManager(IServiceProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException("The provider cannot be null");
        }

        public static OmniManager GetInstance()
        {
            lock (theLock)
            {
                if(instance == null)
                {
                    throw new Exception("OmniManager is not yet setup.");
                }

                return instance;
            }
        }

        internal static OmniManager SetupInstance(IServiceProvider provider)
        {
            lock (theLock)
            {
                if(instance != null)
                {
                    throw new Exception("OmniManager is already setup.");
                }

                instance = new OmniManager(provider);

                return instance;
            }
        }

        public T GetService<T>() where T : class
        {
            return this.provider.GetService(typeof(T)) as T;
        }

        public object GetService(Type theType)
        {
            return this.provider.GetService(theType);
        }
    }
}
