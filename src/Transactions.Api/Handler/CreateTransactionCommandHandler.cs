using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tansactions.Api.Model;
using Transactions.Api.Command;
using Transactions.Api.Service;

namespace Transactions.Api.Handler
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {

        private readonly ITransactionDataService _transactionDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(ILogger<CreateTransactionCommandHandler> logger, ITransactionDataService transactionDataService, IMapper mapper)
        {
            _transactionDataService = transactionDataService ?? throw new ArgumentNullException(nameof(transactionDataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task<Guid> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call db service to create transaction");

            var transaction = _mapper.Map<Transaction>(command);
            return await _transactionDataService.CreateTransaction(transaction);
        }
    }
}