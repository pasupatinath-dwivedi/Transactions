using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tansactions.Model;
using Transactions.Api.Query;

namespace Transactions.Api.Handler
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
    {
        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
             return await Task.Run(()=> new List<Transaction>()
           {
               new Transaction()
               {
                   Amount =30,
                   Owner = new Customer()
                   {
                       Id = Guid.NewGuid()
                   }
               }
            });
        }
    }
}