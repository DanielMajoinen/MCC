using AutoFixture;
using Moq;
using Moula.Data.Dto;
using Moula.Data.Repositories;
using NPoco;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Data.Tests
{
    public class LedgerRepositoryTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDatabase> _mockDatabase = new Mock<IDatabase>();

        [Fact]
        public async Task GetAccountLedgerAsync_ListOfLedgerEntriesRetrieved_Successful()
        {
            // Arrange
            var expectedLedgerEntries = _fixture.CreateMany<Ledger>().ToList();

            _mockDatabase.Setup(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedLedgerEntries);

            var ledgerRepository = new LedgerRepository(_mockDatabase.Object);

            // Act
            var result = await ledgerRepository.GetAccountLedgerAsync(0);

            // Assert
            Assert.Equal(expectedLedgerEntries, result);
            _mockDatabase.Verify(d => d.FetchAsync<Ledger>(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }
    }
}