using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tansactions.Api.Model;

namespace Transactions.Api.Service
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;
        private readonly ILogger _log;


        public TransactionDataService(ILogger<TransactionDataService> log, Database database)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            _log.LogInformation("Get Transactions from cosmos db");
         
            var container = _database.GetContainer(DbConstants.CosmosContainerName);
            var query = container.GetItemQueryIterator<Transaction>(new QueryDefinition(DbConstants.GetTransactionsQuery));
       
            List<Transaction> transactions = new List<Transaction>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                transactions.AddRange(response);
            }
            return transactions;
        }
    }
}
