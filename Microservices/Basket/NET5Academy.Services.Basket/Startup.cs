using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NET5Academy.Services.Basket.Application.Services;
using NET5Academy.Services.Basket.Settings;
using NET5Academy.Shared.Services;

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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NET5Academy.Services.Basket", Version = "v1" });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
