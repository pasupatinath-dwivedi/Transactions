using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transactions.Api.Command;

namespace Transactions.Api.Handler
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        public Task<Guid> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            return Task.Run(()=> Guid.NewGuid());
        }
    }
}