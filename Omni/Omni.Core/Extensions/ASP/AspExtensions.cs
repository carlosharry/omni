using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Omni.Core.Infrastructure.Standard_Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omni.Core.Extensions.ASP
{
    public static class AspExtensions
    {
        public static IApplicationBuilder UseOmni(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException($"{nameof(app)} cannot be null.");
            if (app.ApplicationServices == null) throw new ArgumentException("Your application builder does not have a service manager.");


            OmniManager.SetupInstance(app.ApplicationServices);

            return app;
        }
    }
}
