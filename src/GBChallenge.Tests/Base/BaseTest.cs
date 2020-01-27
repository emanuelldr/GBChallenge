using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using  GBChallenge.Infrastructure.Data.EntityFramework;

namespace GBChallenge.Tests.Base
{
    public class BaseTest
    {
        protected ServiceCollection services;

        public BaseTest()
        {
            var config = InitConfiguration();
            services = new ServiceCollection();
            services.AddDbContext<GBChallengeContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection")));
        }
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();
            return config;
        }
    }
}
