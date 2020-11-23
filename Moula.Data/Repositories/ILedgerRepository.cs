﻿using Moula.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moula.Data.Repositories
{
    public interface ILedgerRepository
    {
        Task<List<Ledger>> GetLedgerByAccountAsync(int accountId);
    }
}
