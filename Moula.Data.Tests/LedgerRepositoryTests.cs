using AutoFixture;
using Moq;
using Moula.Data.Models;
using Moula.Data.Repositories;
using NPoco;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Data.Tests
{
    public class LedgerRepositoryTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDatabase> _mockDatabase = new Mock<IDatabase>();
        private readonly Mock<IDatabaseFactory> _mockDatabaseFactory = new Mock<IDatabaseFactory>();

        [Fact]
        public async Task GetLedgerByAccountAsync_ListOfLedgerEntriesRetrievedSuccessfully()
        {
            // Arrange
            var expectedLedgerEntries = _fixture.CreateMany<Ledger>().ToList();

            _mockDatabaseFactory.Setup(f => f.CreateConnection()).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedLedgerEntries);

            var ledgerRepository = new LedgerRepository(_mockDatabaseFactory.Object);

            // Act
            var result = await ledgerRepository.GetLedgerByAccountAsync(0);

            // Assert
            Assert.Equal(expectedLedgerEntries, result);
            _mockDatabase.Verify(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task GetLedgerByAccountAsync_ExceptionThrownAccessingDatabase()
        {
            // Arrange
            _mockDatabaseFactory.Setup(f => f.CreateConnection()).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            var ledgerRepository = new LedgerRepository(_mockDatabaseFactory.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => ledgerRepository.GetLedgerByAccountAsync(0));
            _mockDatabase.Verify(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}
