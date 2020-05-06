using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EazyTransfer.Shared.Dto
{
    public class CustomerDto
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        //public string Phone { get; set; }
        public IEnumerable Transactions { get; set; }
    }
}
