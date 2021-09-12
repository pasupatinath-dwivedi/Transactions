using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tansactions.Api.Model;
using Transactions.Api.Query;
using Transactions.Api.Service;

namespace Transactions.Api.Handler
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        private readonly ITransactionDataService _transactionDataService;
        private readonly ILogger _logger;

        public GetTransactionsQueryHandler(ILogger<GetTransactionsQueryHandler> logger, ITransactionDataService transactionDataService)
        {
            _transactionDataService = transactionDataService ?? throw new ArgumentNullException(nameof(transactionDataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call db service to get transactions");
            return await _transactionDataService.GetTransactionsAsync();
        }
    }
}