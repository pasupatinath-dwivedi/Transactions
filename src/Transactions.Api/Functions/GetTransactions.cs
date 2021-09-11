using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediatR;
using Transactions.Api.Query;
using System.Threading;

namespace Transactions.Api
{
    public class GetTransactions
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GetTransactions(IMediator mediator, ILogger<GetTransactions> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [FunctionName(nameof(GetTransactions))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.Transactions)] HttpRequest req,
            
             CancellationToken cancellationToken = default)
        {

            _logger.LogInformation("GetTransactions function processed a request.");

            var responseMessage = await _mediator.Send(new GetTransactionsQuery(), cancellationToken);

            return new OkObjectResult(responseMessage);
        }
    }
}
