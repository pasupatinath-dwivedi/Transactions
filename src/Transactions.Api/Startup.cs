using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transactions.Api.Service;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
//using System.Reflection;

[assembly: FunctionsStartup(typeof(Transactions.Api.Startup))]
namespace Transactions.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Services
              .AddOptions<ApplicationSettings>()
              .Configure<IConfiguration>((settings, configuration) => configuration.Bind(settings));
           
            builder.Services.AddMediatR(typeof(Startup));

            ConfigureCosmos(builder);

            builder.Services.AddScoped<ITransactionDataService,TransactionDataService>(services => {

                var logger = services.GetRequiredService<ILogger<TransactionDataService>>();
                var database = services.GetRequiredService<Database>();
               // var productSettings = services.GetRequiredService<IOptionsSnapshot<ProductSettings>>();
                //var cosmosLinqQuery = services.GetRequiredService<ICosmosLinqQuery>();

                return new TransactionDataService(logger, database);
            });

        }

        private void ConfigureCosmos(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(services => {
                var options = new CosmosClientOptions();
                var appSettings = services.GetRequiredService<IOptions<ApplicationSettings>>().Value;
                options.ConnectionMode = ConnectionMode.Gateway;
                var connectionString = appSettings.CosmosDBConnection;

                return new CosmosClient(connectionString, options);
            });

            builder.Services.AddSingleton(services => {
                var cosmosClient = services.GetRequiredService<CosmosClient>();
                var appSettings = services.GetRequiredService<IOptions<ApplicationSettings>>().Value;
                return cosmosClient.GetDatabase(appSettings.DatabaseId);
            });
        }
    }
}
