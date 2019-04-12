using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IssuMgr.API.BO;
using IssuMgr.API.BO.Interfaces;
using IssuMgr.API.DM;
using IssuMgr.API.DM.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace IssuMgr.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // DI
            services.AddTransient<ILblBO, LblBO>();
            services.AddTransient<ILblDM, LblDM>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Title = "Issue Manager API",
                    Version = "v1",
                    Description = "API para Issue Manager",
                    Contact = new Contact {
                        Name = "Eliseo Rodriguez",
                        Email = "eliseo_rodriguez@latisinformatica.com",
                        Url = "htts://www.latisinformatica.com"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Issue Manager API V1");
            });
            app.UseMvc();
        }
    }
}
