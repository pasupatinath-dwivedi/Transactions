using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Transactions.Api.Service;

[assembly: FunctionsStartup(typeof(Transactions.Api.Startup))]
namespace Transactions.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            ConfigureCosmos(builder);
            ConfigureServices(builder);
        }

        private static void ConfigureServices(IFunctionsHostBuilder builder)
        {
            builder.Services
              .AddOptions<ApplicationSettings>()
              .Configure<IConfiguration>((settings, configuration) => configuration.Bind(settings));

            builder.Services.AddMediatR(typeof(Startup));
            builder.Services.AddAutoMapper(typeof(Startup));

            builder.Services.AddScoped<ITransactionDataService, TransactionDataService>(services =>
            {
                var logger = services.GetRequiredService<ILogger<TransactionDataService>>();
                var database = services.GetRequiredService<Database>();
                return new TransactionDataService(logger, database);
            });
        }

        private void ConfigureCosmos(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(services =>
            {
                var options = new CosmosClientOptions()
                {
                    SerializerOptions = new CosmosSerializationOptions()
                    {
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    }
                };
                var appSettings = services.GetRequiredService<IOptions<ApplicationSettings>>().Value;
                options.ConnectionMode = ConnectionMode.Gateway;

                var connectionString = appSettings.CosmosDBConnection;

                return new CosmosClient(connectionString, options);
            });

            builder.Services.AddSingleton(services =>
            {
                var cosmosClient = services.GetRequiredService<CosmosClient>();
                var appSettings = services.GetRequiredService<IOptions<ApplicationSettings>>().Value;
                return cosmosClient.GetDatabase(appSettings.DatabaseId);
            });
        }
    }
}
