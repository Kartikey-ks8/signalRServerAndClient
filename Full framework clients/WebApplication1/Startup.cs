using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(WebApplication1.Startup))]

namespace WebApplication1
{
    public class Startup
    {
        public void Configuration(IApplicationBuilder app)
        {
         
            app.UseOwinApp(owinApp =>
            {
                owinApp.MapSignalR();
            });
         
        }
    }
}
