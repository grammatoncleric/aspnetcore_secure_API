using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EazyTransfer.Shared
{
    public class FundAccountViewModel
    {
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string AccountNumber{ get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }
    }
}
