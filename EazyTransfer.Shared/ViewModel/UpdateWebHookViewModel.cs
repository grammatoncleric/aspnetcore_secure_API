using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EazyTransfer.Shared
{
    public class UpdateWebHookViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string HashCode { get; set; }

        [Required]
        public string WebHookURL { get; set; }
    }
}
