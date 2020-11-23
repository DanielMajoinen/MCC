using AutoFixture;
using Moq;
using Moula.Data.Models;
using Moula.Data.Repositories;
using NPoco;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Data.Tests
{
    public class AccountRepositoryTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDatabase> _mockDatabase = new Mock<IDatabase>();
        private readonly Mock<IDatabaseFactory> _mockDatabaseFactory = new Mock<IDatabaseFactory>();

        [Fact]
        public async Task GetAccountAsync_AccountRetrievedSuccessfully()
        {
            // Arrange
            var expectedAccount = _fixture.Create<Account>();

            _mockDatabaseFactory.Setup(f => f.CreateConnection()).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(d => d.SingleOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedAccount);

            var accountRepository = new AccountRepository(_mockDatabaseFactory.Object);

            // Act
            var result = await accountRepository.GetAccountAsync(0);

            // Assert
            Assert.Equal(expectedAccount, result);
            _mockDatabase.Verify(d => d.SingleOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task GetAccountAsync_ExceptionThrownAccessingDatabase()
        {
            // Arrange
            _mockDatabaseFactory.Setup(f => f.CreateConnection()).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(d => d.SingleOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            var accountRepository = new AccountRepository(_mockDatabaseFactory.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => accountRepository.GetAccountAsync(0));
            _mockDatabase.Verify(d => d.SingleOrDefaultAsync<Account>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
