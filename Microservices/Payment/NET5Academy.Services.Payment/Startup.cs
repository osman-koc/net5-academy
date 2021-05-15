using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NET5Academy.Shared.Config;

namespace NET5Academy.Services.Payment
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ISwaggerSettings _swaggerSettings;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _swaggerSettings = new SwaggerSettings()
            {
                ApiName = configuration.GetValue<string>("Swagger:ApiName"),
                Version = configuration.GetValue<string>("Swagger:Version"),
                EndpointUrl = configuration.GetValue<string>("Swagger:EndpointUrl"),
                EndpointName = configuration.GetValue<string>("Swagger:EndpointName")
            };
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_swaggerSettings.Version, new OpenApiInfo { Title = _swaggerSettings.ApiName, Version = _swaggerSettings.Version });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(_swaggerSettings.EndpointUrl, _swaggerSettings.EndpointName));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
