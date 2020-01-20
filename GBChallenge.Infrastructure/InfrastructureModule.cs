using Autofac;
namespace GBChallenge.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
        }
    }
}
