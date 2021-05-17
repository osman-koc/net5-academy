using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NET5Academy.Services.Discount.Application.Services;
using NET5Academy.Services.Discount.Data.Repositories;
using NET5Academy.Shared.Constants;
using NET5Academy.Shared.Config;
using NET5Academy.Shared.Services;
using System.IdentityModel.Tokens.Jwt;

namespace NET5Academy.Services.Discount
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
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(OkIdentityConstants.UserIdKey);

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_swaggerSettings.Version, new OpenApiInfo { Title = _swaggerSettings.ApiName, Version = _swaggerSettings.Version });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = _configuration["IdentityServerUri"];
                    options.Audience = OkIdentityConstants.ResourceName.DiscountAPI;
                    options.RequireHttpsMetadata = false;
                });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IDiscountService, DiscountService>();
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
