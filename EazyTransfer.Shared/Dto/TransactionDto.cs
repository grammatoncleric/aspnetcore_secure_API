using System;
using System.Collections.Generic;
using System.Text;

namespace EazyTransfer.Shared.Dto
{
    public class TransactionDto
    {
        public int AccountNumber { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
