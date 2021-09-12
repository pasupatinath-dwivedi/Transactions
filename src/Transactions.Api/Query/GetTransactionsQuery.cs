using MediatR;
using System.Collections.Generic;
using Tansactions.Api.Model;

namespace Transactions.Api.Query
{
    public class GetTransactionsQuery : IRequest<List<Transaction>>
    {

    }
}
