using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NET5Academy.Web.Infrastructure.Handlers;
using NET5Academy.Web.Models.Config;
using NET5Academy.Web.Services;
using System;

namespace NET5Academy.Web
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
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<OkServiceSettings>(_configuration.GetSection("OkServiceSettings"));
            var okServiceSettings = _configuration.GetSection("OkServiceSettings").Get<OkServiceSettings>();
            services.AddScoped<OkServiceSettings>(x => okServiceSettings);

            services.AddHttpContextAccessor();

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IUserService, UserService>(options =>
            {
                options.BaseAddress = new Uri(okServiceSettings.IdentityServer.BaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30);
                    options.SlidingExpiration = true;
                    options.Cookie.Name = "auth_net5_cookie";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
