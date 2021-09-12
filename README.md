# Transactions
Azure Function App to expose 3 endpoints
  1.	GET /transactions
  2. POST  /transactions
  3. PUT /transactions/{transactionsID}

 Action | Method | Route
------------ | ------------- |--------
Get All Transactions	|GET request. Returns success all transactions as json array.	| GET /transactions (sample url : http://localhost:7071/api/transactions)
Crete transaction |POST request. Returns 200 OK on successful creation.| POST /transactions (sample url :  http://localhost:7071/api/transactions)
Update a transactions |PUT request. Returns 200 OK on successful update.| PUT /transactions/{transactionID} (sample url :  http://localhost:7071/api/transactions/{transactionID})

# Build and Test
- Source code is found in the src/ folder, the main application is Transactions.Api. A solution file for Visual Studio is in the root
- Tests are found in the tests/ folder and can be run on Visual Studio

## Sample local.settings.json for debugging locally
> The following is prepopulated with connection string for local storage and cosmos emulators
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "DatabaseId": "TransactionDemo",
    "CosmosDBConnection": "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
  }
}
```
