using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using MediatR;
//using System.Reflection;

[assembly: FunctionsStartup(typeof(Transactions.Api.Startup))]
namespace Transactions.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Services.AddMediatR(typeof(Startup));
        }
    }
}
