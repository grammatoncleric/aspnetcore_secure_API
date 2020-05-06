using EazyTransfer.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace EazyTransfer.Shared
{
    public class GetTransactionResponse
    {
        public IEnumerable<CustomerDto> Customers { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
