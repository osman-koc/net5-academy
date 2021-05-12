using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NET5Academy.Services.Basket.Application.Services;
using NET5Academy.Services.Basket.Settings;
using NET5Academy.Shared.Constants;
using NET5Academy.Shared.Services;
using System.IdentityModel.Tokens.Jwt;

namespace NET5Academy.Services.Basket
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(OkIdentityConstans.UserIdKey);
            
            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NET5Academy.Services.Basket", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["IdentityServerUri"];
                    options.Audience = OkIdentityConstans.ResourceName.BasketAPI;
                    options.RequireHttpsMetadata = false;
                });

            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.AddSingleton<IRedisSettings>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

            services.AddSingleton<IRedisService, RedisService>();
            services.AddSingleton<IBasketService, BasketService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NET5Academy.Services.Basket v1"));
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
