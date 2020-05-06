using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Models
{
    public class EazyTransferDbContext: DbContext
    {
        public EazyTransferDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MerchantAccount> MerchantAccounts { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }


}
