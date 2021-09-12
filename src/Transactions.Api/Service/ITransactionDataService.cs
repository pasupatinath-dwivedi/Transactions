using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tansactions.Api.Model;

namespace Transactions.Api.Service
{
    public interface ITransactionDataService
    {
        Task<List<Transaction>> GetTransactionsAsync();

        Task<Guid> CreateTransaction(Transaction transaction);

        Task<Guid> UpdateTransactionAsync(Transaction transaction);
    }
}