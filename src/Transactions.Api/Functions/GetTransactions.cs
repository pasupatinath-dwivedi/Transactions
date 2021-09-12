using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transactions.Api.Query;

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
