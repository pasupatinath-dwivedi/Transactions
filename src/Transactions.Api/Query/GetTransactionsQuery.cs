using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Tansactions.Api.Model;

namespace Transactions.Api.Query
{
   public class GetTransactionsQuery :IRequest<List<Transaction>>
    {

    }
}
