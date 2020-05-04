using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Models
{
    public class CustomerAccount
    {
        public int CustomerAccountId { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public int MerchantAccountId { get; set; }
        public int UserId { get; set; }  
        public string Phone { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
