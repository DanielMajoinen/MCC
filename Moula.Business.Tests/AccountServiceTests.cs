using AutoFixture;
using Moq;
using Moula.Business.Services;
using Moula.Data.Models;
using Moula.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Business.Tests
{
    public class AccountServiceTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IAccountRepository> _accountRepository = new Mock<IAccountRepository>();
        private readonly Mock<ILedgerRepository> _ledgerRepository = new Mock<ILedgerRepository>();

        [Fact]
        public async Task GetAccountLedgerAsync_AccountLedgerRetrievedSuccessfully()
        {
            // Arrange
            var expectedAccount = _fixture.Create<Account>();
            var expectedLedger = _fixture.CreateMany<Ledger>().ToList();

            _accountRepository.Setup(a => a.GetAccountAsync(It.IsAny<int>())).ReturnsAsync(expectedAccount);
            _ledgerRepository.Setup(l => l.GetLedgerByAccountAsync(It.IsAny<int>())).ReturnsAsync(expectedLedger);

            var accountService = new AccountService(_accountRepository.Object, _ledgerRepository.Object);

            // Act
            var result = await accountService.GetAccountLedgerAsync(0);

            // Assert
            Assert.Equal(expectedAccount.Id, result.Account);
            Assert.Equal(expectedAccount.Balance, result.Balance);
            Assert.True(expectedLedger.All(l => 
            {
                var r = result.PaymentHistory.FirstOrDefault(r => r.Id == l.Id);
                return r != null
                    && l.Date == r.Date
                    && l.Amount == r.Amount
                    && l.Status == r.Status
                    && l.ClosedReason == r.ClosedReason;
            }));

            _accountRepository.Verify(a => a.GetAccountAsync(It.IsAny<int>()), Times.Once);
            _ledgerRepository.Verify(a => a.GetLedgerByAccountAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAccountLedgerAsync_AccountReturnsNull_NullReturned()
        {
            // Arrange
            var expectedLedger = _fixture.CreateMany<Ledger>().ToList();

            _accountRepository.Setup(a => a.GetAccountAsync(It.IsAny<int>())).ReturnsAsync((Account) null);
            _ledgerRepository.Setup(l => l.GetLedgerByAccountAsync(It.IsAny<int>())).ReturnsAsync(expectedLedger);

            var accountService = new AccountService(_accountRepository.Object, _ledgerRepository.Object);

            // Act
            var result = await accountService.GetAccountLedgerAsync(0);

            // Assert
            Assert.Null(result);
            _accountRepository.Verify(a => a.GetAccountAsync(It.IsAny<int>()), Times.Once);
            _ledgerRepository.Verify(a => a.GetLedgerByAccountAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAccountLedgerAsync_LedgerReturnsEmpty_EmptyPaymentHistoryReturned()
        {
            // Arrange
            var expectedAccount = _fixture.Create<Account>();

            _accountRepository.Setup(a => a.GetAccountAsync(It.IsAny<int>())).ReturnsAsync(expectedAccount);
            _ledgerRepository.Setup(l => l.GetLedgerByAccountAsync(It.IsAny<int>())).ReturnsAsync(new List<Ledger>());

            var accountService = new AccountService(_accountRepository.Object, _ledgerRepository.Object);

            // Act
            var result = await accountService.GetAccountLedgerAsync(0);

            // Assert
            Assert.Equal(expectedAccount.Id, result.Account);
            Assert.Equal(expectedAccount.Balance, result.Balance);
            Assert.False(result.PaymentHistory.Any());
            _accountRepository.Verify(a => a.GetAccountAsync(It.IsAny<int>()), Times.Once);
            _ledgerRepository.Verify(a => a.GetLedgerByAccountAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAccountLedgerAsync_LedgerReturnsNull_EmptyPaymentHistoryReturned()
        {
            // Arrange
            var expectedAccount = _fixture.Create<Account>();

            _accountRepository.Setup(a => a.GetAccountAsync(It.IsAny<int>())).ReturnsAsync(expectedAccount);
            _ledgerRepository.Setup(l => l.GetLedgerByAccountAsync(It.IsAny<int>())).ReturnsAsync((List<Ledger>) null);

            var accountService = new AccountService(_accountRepository.Object, _ledgerRepository.Object);

            // Act
            var result = await accountService.GetAccountLedgerAsync(0);

            // Assert
            Assert.Equal(expectedAccount.Id, result.Account);
            Assert.Equal(expectedAccount.Balance, result.Balance);
            Assert.False(result.PaymentHistory.Any());
            _accountRepository.Verify(a => a.GetAccountAsync(It.IsAny<int>()), Times.Once);
            _ledgerRepository.Verify(a => a.GetLedgerByAccountAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
