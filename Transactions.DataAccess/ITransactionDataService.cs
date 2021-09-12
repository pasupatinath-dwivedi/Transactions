using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tansactions.Model;

namespace Transactions.DataAccess
{
    public interface ITransactionDataService
    {
        /// <summary>
        /// Get all transactions from the Transactions Collection
        /// </summary>
        /// <returns></returns>
        Task<List<Transaction>> GetTransactions();
    }
}
