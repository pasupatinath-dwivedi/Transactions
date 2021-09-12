using Moq;
using Transactions.Api.Service;
using Xunit;
using System.Collections.Generic;
using Tansactions.Api.Model;
using System.Threading.Tasks;
using Transactions.Api.Handler;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using AutoMapper;

namespace Transactions.Api.Test
{
    public class CreateTransactionsHandlerTests
    {
        [Fact]
         public async Task CommandHandler_HandleHappyPath()
        {
            //Arrange
            var transactionDataService = new Mock<ITransactionDataService>();
            var logger = new Mock<ILogger<CreateTransactionCommandHandler>>();
            var mapper = new Mock<IMapper>();

            transactionDataService.Setup(x => x.CreateTransaction(It.IsAny<Transaction>())).
                            Returns(Task.FromResult(System.Guid.NewGuid()))
                            .Verifiable();
           
            var handler = new CreateTransactionCommandHandler(logger.Object, transactionDataService.Object,mapper.Object);

            //Act
            var response =await handler.Handle(new Command.CreateTransactionCommand(),  default);

            //Assert
            transactionDataService.Verify();
            response.Should().NotBeEmpty();
        }
    }
}