using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GBChallenge.API.Helpers.Defaults.Extensions;
using GBChallenge.Core;
using GBChallenge.Core.Domain.Entities.Settings;
using GBChallenge.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCustomDbContext(Configuration);
            services.AddIdentity();

            var settingsSection = Configuration.GetSection("GBChallengeSettings");
            services.Configure<GBChallengeSettings>(settingsSection);
            var gbChallengeSettings = settingsSection.Get<GBChallengeSettings>();

            services.AddJWT(Configuration, gbChallengeSettings.TokenSettings);

            //Swagger Components
            services
                .AddVersionedApiExplorer()
                .AddApiVersioning(o => { o.ReportApiVersions = true; o.AssumeDefaultVersionWhenUnspecified = true; })
                .AddCustomVersionedApiExplorer(Configuration)
                .AddSwagger(Configuration);

            // DI/IoC
            var builder = new ContainerBuilder();
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new CoreModule());
            

            builder.Populate(services);
            var container = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
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

                c.DocumentTitle = "Grupo Boticario Api";
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
