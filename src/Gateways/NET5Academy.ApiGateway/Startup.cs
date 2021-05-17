using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NET5Academy.Shared.Constants;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace NET5Academy.ApiGateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication().AddJwtBearer("GatewayAuthenticationSchema", options =>
            {
                options.Authority = _configuration["IdentityServerUri"];
                options.Audience = OkIdentityConstants.ResourceName.ApiGateway;
                options.RequireHttpsMetadata = false;
            });

            services.AddOcelot();
        }

        async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            await app.UseOcelot();
        }
    }
}
