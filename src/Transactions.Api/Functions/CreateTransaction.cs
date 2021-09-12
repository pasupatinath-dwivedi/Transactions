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
using Transactions.Api.Command;
using Transactions.Api.Extensions;
using System.Threading;
using System.Text.Json;

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
            _logger.LogInformation("C# HTTP trigger function processed a request.");
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
