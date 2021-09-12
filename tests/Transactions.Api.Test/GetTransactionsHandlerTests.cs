using Moq;
using Transactions.Api.Service;
using Xunit;
using System.Collections.Generic;
using Tansactions.Api.Model;
using System.Threading.Tasks;
using Transactions.Api.Handler;
using Microsoft.Extensions.Logging;
using FluentAssertions;
namespace Transactions.Api.Test
{
    
    public class GetTransactionsHandlerTests
    {
        [Fact]
         public async Task CommandHandler_HandleHappyPath()
        {
            //Arrange
            var transactionDataService = new Mock<ITransactionDataService>();
            var logger = new Mock<ILogger<GetTransactionsQueryHandler>>();

            transactionDataService.Setup(x => x.GetTransactionsAsync()).
                            Returns(Task.FromResult(new List<Transaction>() { new Transaction() }))
                            .Verifiable();
           
            var handler = new GetTransactionsQueryHandler(logger.Object, transactionDataService.Object);

            //Act
            var response =await handler.Handle(new Query.GetTransactionsQuery(), default);

            //Assert
            transactionDataService.Verify();
            response.Should().HaveCount(1);
        }

    }
}