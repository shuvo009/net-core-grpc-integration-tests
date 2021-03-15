using System;
using System.Linq;
using Grpc.DotNetCore.Repository.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.DotNetCore.IntegrationTest.Setup
{
    public class TestHost : WebApplicationFactory<Startup>
    {
        private readonly string _databaseName;

        public TestHost()
        {
            _databaseName = Guid.NewGuid().ToString();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContext =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ProductDbContext>));
                if (dbContext != null)
                    services.Remove(dbContext);

                services.AddDbContext<ProductDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase(_databaseName);
                    opt.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
                services.AddTransient<ProductDbContext>();
            });
        }
    }
}