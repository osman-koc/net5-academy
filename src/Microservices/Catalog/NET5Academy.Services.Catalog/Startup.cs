using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NET5Academy.Services.Catalog.Application.Services;
using NET5Academy.Shared.Constants;
using NET5Academy.Shared.Config;

namespace NET5Academy.Services.Catalog
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
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_swaggerSettings.Version, new OpenApiInfo { Title = _swaggerSettings.ApiName, Version = _swaggerSettings.Version });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = _configuration["IdentityServerUri"];
                    options.Audience = OkIdentityConstants.ResourceName.CatalogAPI;
                    options.RequireHttpsMetadata = false;
                });

            services.AddAutoMapper(typeof(Startup));

            services.Configure<MongoSettings>(_configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoSettings>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value;
            });

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
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
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
