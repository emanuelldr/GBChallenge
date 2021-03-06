﻿using Autofac;
using GBChallenge.Core.Domain.Interfaces;
using GBChallenge.Infrastructure.CrossCutting.ExternalClients;
using GBChallenge.Infrastructure.Data.Repositories;

namespace GBChallenge.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CashBackClient>().As<ICashBackClient>().InstancePerLifetimeScope();
            builder.RegisterType<RevendedorRepository>().As<IRevendedorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompraRepository>().As<ICompraRepository>().InstancePerLifetimeScope();
        }
    }
}
