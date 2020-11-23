using System;

namespace Moula.Data.Dto
{
    public class Ledger
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string ClosedReason { get; set; }
    }
}
