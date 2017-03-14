using Microsoft.Extensions.DependencyInjection;
using Omni.Core.Infrastructure.Interfaces;
using Omni.Core.Service;
using Omni.ServiceProviders.EntityFramework.Factory;
using Omni.ServiceProviders.EntityFramework.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni.ServiceProviders.EntityFramework.Extensions.ASP
{
    public static class AspExtensions
    {
        public static IServiceCollection UseEntityFrameworkWithOmni(this IServiceCollection services, Func<DbContext> context)
        {
            if (services == null) throw new ArgumentNullException("IServiceCollection cannot be null");
            if (context == null) throw new ArgumentNullException("The Func<DbContext> cannot be null");

            ContextFactory.SetupInstance(context);

            services.AddTransient<IDataServiceFactory, EntityDataServiceFactory>();

            return services;
        }
    }
}
