using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GBChallenge.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            //builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
        }
    }
}
