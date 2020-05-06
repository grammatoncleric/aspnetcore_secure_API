using System;
using System.Collections.Generic;
using System.Text;

namespace EazyTransfer.Shared
{
    public class NewAccountResponse
    {
        public string AccountNumber { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }
}
