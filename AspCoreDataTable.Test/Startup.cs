using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreDataTable.Core.Configuration;
using AspCoreDataTable.Core.ConfirmBuilder;
using AspCoreDataTable.Core.General.Enums;
using AspCoreDependency.Core.Concrete;
using AspCoreDependency.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspCoreDataTable.Test
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
            services.AddControllersWithViews();

            services.AutoBind();

            services.AddDataTable(option =>
            {
                option.storageType = EnumStorage.Cookie;
                option.confirmType = ConfirmType.BootBox;
            });

            DependencyResolver.Init(BuildServices(services));
        }

        private ServiceProvider BuildServices(IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
