using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GBChallenge.Core.BusinessServices;
using GBChallenge.Core.Domain.Interfaces;

namespace GBChallenge.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutenticacaoService>().As<IAutenticacaoService>().InstancePerLifetimeScope();
            builder.RegisterType<RevendedorService>().As<IRevendedorService>().SingleInstance();
        }
    }
}
