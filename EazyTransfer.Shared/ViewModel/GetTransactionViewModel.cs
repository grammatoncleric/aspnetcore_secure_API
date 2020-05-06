using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EazyTransfer.Shared
{
    public class GetTransactionViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string HashCode { get; set; }

        [Required]
        public DateTime Date { get; set; }

        //[Required]
        //public DateTime EndDate { get; set; }
    }
}
