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
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Guid>
    {
        private readonly ITransactionDataService _transactionDataService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UpdateTransactionCommandHandler(ILogger<UpdateTransactionCommandHandler> logger, ITransactionDataService transactionDataService, IMapper mapper)
        {
            _transactionDataService = transactionDataService ?? throw new ArgumentNullException(nameof(transactionDataService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public async Task<Guid> Handle(UpdateTransactionCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call db service to update transaction");

            var transaction = _mapper.Map<Transaction>(command);
            return await _transactionDataService.UpdateTransactionAsync(transaction);
        }
    }
}
