using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moula.Api.Controllers;
using Moula.Business.Services;
using Moula.Contracts;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Moula.Api.Tests
{
    public class AccountControllerTests
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IAccountService> _accountService = new Mock<IAccountService>();
        private readonly Mock<ILogger<AccountController>> _logger = new Mock<ILogger<AccountController>>();

        [Fact]
        public async Task Get_ReturnsAccountLedgerSuccessfully()
        {
            // Arrange
            var expectedAccountLedger = _fixture.Create<AccountLedger>();

            _accountService.Setup(a => a.GetAccountLedgerAsync(It.IsAny<int>())).ReturnsAsync(expectedAccountLedger);

            var accountController = new AccountController(_accountService.Object, _logger.Object);

            // Act
            var result = (OkObjectResult) await accountController.Get(0);

            // Assert
            Assert.Equal(expectedAccountLedger, result.Value);
            _accountService.Verify(a => a.GetAccountLedgerAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_ExceptionCaught_LogsAndReturnsInternalServiceError()
        {
            // Arrange
            var expectedAccountLedger = _fixture.Create<AccountLedger>();

            _accountService.Setup(a => a.GetAccountLedgerAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            var accountController = new AccountController(_accountService.Object, _logger.Object);

            // Act
            var result = (StatusCodeResult) await accountController.Get(0);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            _accountService.Verify(a => a.GetAccountLedgerAsync(It.IsAny<int>()), Times.Once);
            _logger.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>) It.IsAny<object>()), Times.Exactly(1));
        }

        [Fact]
        public async Task Get_AccountNotFound_Returns404()
        {
            // Arrange
            var expectedAccountLedger = _fixture.Create<AccountLedger>();

            _accountService.Setup(a => a.GetAccountLedgerAsync(It.IsAny<int>())).ReturnsAsync((AccountLedger) null);

            var accountController = new AccountController(_accountService.Object, _logger.Object);

            // Act
            var result = (StatusCodeResult) await accountController.Get(0);

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            _accountService.Verify(a => a.GetAccountLedgerAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
