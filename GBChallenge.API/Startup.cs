using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBChallenge.API.Data.DataContext;
using GBChallenge.API.Helpers.Defaults.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GBChallenge.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCustomDbContext(Configuration);
            services.AddIdentity();

            var settingsSection = Configuration.GetSection("GBChallengeSettings");
            services.Configure<GBChallengeSettings>(settingsSection);
            var gbChallengeSettings = settingsSection.Get<GBChallengeSettings>();

            services.AddJWT(Configuration, gbChallengeSettings);

            //Swagger Components
            services
                .AddVersionedApiExplorer()
                .AddApiVersioning(o => { o.ReportApiVersions = true; o.AssumeDefaultVersionWhenUnspecified = true; })
                .AddCustomVersionedApiExplorer(Configuration)
                .AddSwagger(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                }

                c.DocumentTitle = "Token Api";
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
