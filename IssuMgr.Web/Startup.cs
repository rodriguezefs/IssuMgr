using IssuMgr.DM.Interfaces;
using IssuMgr.BO;
using IssuMgr.BO.Interfaces;
using IssuMgr.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using IssuMgr.Web.Resources;

namespace IssuMgr.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Localization
            // http://ziyad.info/en/articles/15-Localizing_Views
            app.UseRequestLocalization(LocalizationOptions());

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            // Localization
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddViewLocalization(options => options.ResourcesPath = "Resources")
                .AddRazorPagesOptions(o => {
                    o.Conventions.Add(new CultureTemplateRouteModelConvention());
                });

            // DI
            services.AddSingleton<CultureLocalizer>();

            services.AddTransient<ILblBO, LblBO>();
            services.AddTransient<IIssuBO, IssuBO>();

            services.AddTransient<ILblDM, LblDM>();
            services.AddTransient<IIssuDM, IssuDM>();

            services.AddRazorPages()
                .AddNewtonsoftJson();
        }
        private RequestLocalizationOptions LocalizationOptions() {

            RequestLocalizationOptions lxLoc = new RequestLocalizationOptions();
            lxLoc.SupportedCultures.Add(new CultureInfo("en-US"));
            lxLoc.SupportedUICultures.Add(new CultureInfo("en-US"));
            lxLoc.SupportedCultures.Add(new CultureInfo("es-ES"));
            lxLoc.SupportedUICultures.Add(new CultureInfo("es-ES"));
            lxLoc.DefaultRequestCulture = new RequestCulture("en-US");

            return lxLoc;
        }
    }
}
