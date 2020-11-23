using System.Collections.Generic;

namespace Moula.Contracts
{
    public class AccountLedger
    {
        public int Account { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Payment> PaymentHistory { get; set; }
    }
}
