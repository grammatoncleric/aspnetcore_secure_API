using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EazyTransfer.Shared
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string BusinessName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string BusinessPhone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string BusinessAddress { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string MerchantWebHook { get; set; }

    }
}
