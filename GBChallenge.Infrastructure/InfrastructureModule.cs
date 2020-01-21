using Autofac;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Infrastructure.Data.Repositories;

namespace GBChallenge.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RevendedorRepository>().As<IRevendedorRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
        }
    }
}
