using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Owin.Builder;
using Microsoft.Owin.BuilderProperties;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class OwinExtensions
{
    public static IApplicationBuilder UseOwinApp(this IApplicationBuilder app, Action<IAppBuilder> configuration)
    {
        return app.UseOwin(setup => setup(next =>
        {
            IAppBuilder owinApp = new AppBuilder();

            var aspNetCoreLifetime = (IHostApplicationLifetime)app.ApplicationServices.GetService(typeof(IHostApplicationLifetime));

            var owinAppProperties = new AppProperties(owinApp.Properties)
            {
                OnAppDisposing = aspNetCoreLifetime?.ApplicationStopping ?? CancellationToken.None,
                DefaultApp = next
            };

            configuration(owinApp);

            return owinApp.Build<Func<IDictionary<string, object>, Task>>();
        }));
    }
}
