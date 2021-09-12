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
        private readonly Database _database;
        private readonly ILogger _log;
        private readonly Container _container;

        public TransactionDataService(ILogger<TransactionDataService> log, Database database)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _container = _database.GetContainer(DbConstants.CosmosContainerName);

        }

        public async Task<Guid> CreateTransaction(Transaction transaction)
        {
            _log.LogInformation("Create a transaction in cosmos db for id: ", transaction.Id);

            await _container.CreateItemAsync<Transaction>(transaction, new PartitionKey(transaction.Id.ToString()));
            return transaction.Id;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            _log.LogInformation("Get Transactions from cosmos db");

            var query = _container.GetItemQueryIterator<Transaction>(new QueryDefinition(DbConstants.GetTransactionsQuery));

            List<Transaction> transactions = new List<Transaction>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                transactions.AddRange(response);
            }
            return transactions;
        }

        public async Task<Guid> UpdateTransactionAsync(Transaction transaction)
        {
            _log.LogInformation("Update Transaction in cosmos db for id: ", transaction.Id);

            await _container.UpsertItemAsync<Transaction>(transaction, new PartitionKey(transaction.Id.ToString()));
            return transaction.Id;
        }
    }
}