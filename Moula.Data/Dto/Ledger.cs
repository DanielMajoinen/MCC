using NPoco;
using System;

namespace Moula.Data.Dto
{
    [TableName("Ledger")]
    [PrimaryKey("Id")]
    public class Ledger
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string ClosedReason { get; set; }
    }
}
