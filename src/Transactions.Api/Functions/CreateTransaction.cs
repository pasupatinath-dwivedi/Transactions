using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transactions.Api.Command;
using Transactions.Api.Extensions;

namespace Transactions.Api.Functions
{
    public class CreateTransaction
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;


        public CreateTransaction(IMediator mediator, ILogger<GetTransactions> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [FunctionName(nameof(CreateTransaction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = Routes.Transactions)] HttpRequest req, CancellationToken cancellationToken = default
          )
        {
            _logger.LogInformation("CreateTransaction function processed a request.");
            var transactionCommand = req.FromBody<CreateTransactionCommand>();

            if (transactionCommand == null)
            {
                return new BadRequestResult();
            }

            var responseMessage = _mediator.Send(transactionCommand, cancellationToken);
            return new OkObjectResult(responseMessage.Result);
        }
    }
}
