using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api;

namespace UrlShortner.Tests
{
    public class ApiWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public readonly TestcontainerDatabase _database = new TestcontainersBuilder<PostgreSqlTestcontainer>()
        .WithDatabase(new PostgreSqlTestcontainerConfiguration
        {
            Database = "testDb",
            Username = "testUser",
            Password = "doesnt_matter"
        }).Build();

        public async Task InitializeAsync()
        {
            await _database.StartAsync();
            var result = await _database.ExecAsync(new[]
             {
                  "/bin/sh", "-c",
                  "psql -U postgres -c \"CREATE EXTENSION plv8; SELECT extversion FROM pg_extension WHERE extname = 'plv8';\""
              });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            });
            builder.ConfigureServices(services =>
            {
                services.AddMarten(opt =>
                {
                    opt.Connection(_database.ConnectionString);
                });
            });
            

        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _database.StopAsync();
        }
    }
}
