using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string CustomerAccountId { get; set; }
        public string Meta { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
