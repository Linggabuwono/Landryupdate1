using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddControllersWithViews();*/

            /*services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });*/
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/NewViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                /*options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;*/
            });
            services.AddDbContext<Models.LaundryContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Default")));
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;
                if (url.Contains("/Bos/Karyawans"))
                {
                    context.Request.Path = "/Karyawans";
                }
                await next();
            });

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;

                if(url.Contains("/Bos/Pesanans"))
                {
                    context.Request.Path = "/Pesanans";
                }
                await next();
               
            });

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;

                if (url.Contains("/Bos/DetailPelayanans"))
                {
                    context.Request.Path = "/DetailPelayanans";
                }
                await next();

            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ReadOnly}/{action=Index}/{id?}");
            });


        }
    }
}
