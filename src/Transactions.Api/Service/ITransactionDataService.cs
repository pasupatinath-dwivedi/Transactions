using System;
using System.Collections.Generic;
using System.Text;
using Tansactions.Api.Model;
using System.Threading.Tasks;

namespace Transactions.Api.Service
{
    public interface ITransactionDataService
    {
        Task<List<Transaction>> GetTransactionsAsync();
    }
}