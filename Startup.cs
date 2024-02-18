using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Pictionary.Hubs;

namespace YourNamespace
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add SignalR services
            services.AddSignalR();
            
            // Add other services as needed
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure SignalR
            app.UseRouting();
            
            // Use endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<PictionaryHub>("/pictionaryhub");
                
                // Map controllers, if any
            });

            // Use other middleware as needed
        }
    }
}
