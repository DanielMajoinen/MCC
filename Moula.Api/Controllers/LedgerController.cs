using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moula.Contracts;
using System;
using System.Collections.Generic;

namespace Moula.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerController : ControllerBase
    {
        private readonly ILogger<LedgerController> _logger;

        public LedgerController(ILogger<LedgerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Ledger> Get()
        {
            throw new NotImplementedException();
        }
    }
}
