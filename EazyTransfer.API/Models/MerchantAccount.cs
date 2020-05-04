using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Models
{
    public class MerchantAccount
    {
        public int MerchantAccountId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessAddress { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string WebHook { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }




    }
}
