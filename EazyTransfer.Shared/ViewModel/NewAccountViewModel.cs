using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EazyTransfer.Shared
{
    public class NewAccountViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Phone { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string HashCode { get; set; }
    }
}
