using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Transactions.Api.Extensions;
using Transactions.Api.Command;
using System.Threading;

namespace Transactions.Api.Functions
{
    public class UpdateTransaction
    {

        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public UpdateTransaction(IMediator mediator, ILogger<GetTransactions> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [FunctionName(nameof(UpdateTransaction))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = Routes.Update)] HttpRequest req, string transactionID, CancellationToken cancellationToken)

        {
            _logger.LogInformation("C# HTTP trigger function processed a request {transactionId}.", transactionID);

            var updateTransactionCommand = req.FromBody<UpdateTransactionCommand>();

            if (updateTransactionCommand == null || !Guid.TryParse(transactionID, out _))
            {
                return new BadRequestResult();
            }

            var result = await _mediator.Send(updateTransactionCommand, cancellationToken);

            return new OkObjectResult(result);
        }
    }
}
