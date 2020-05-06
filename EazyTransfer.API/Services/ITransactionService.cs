using EazyTransfer.API.Models;
using EazyTransfer.Shared;
using EazyTransfer.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Services
{
    public interface ITransactionService
    {
        Task<GetTransactionResponse> GetTransactionAsync(GetTransactionViewModel model);

    }
    public class TransactionService : ITransactionService
    {
        private readonly EazyTransferDbContext _context;

        public TransactionService(EazyTransferDbContext context)
        {
            _context = context;
        }

        public async Task<GetTransactionResponse> GetTransactionAsync(GetTransactionViewModel model)
        {
            try
            {
                //get merchant
                var merchantId = _context.MerchantAccounts.FirstOrDefault(x => x.ApiKey == model.HashCode).MerchantAccountId;

                if (merchantId != 0)
                {

                    //get all customer account for merchant
                    var customers = _context.CustomerAccounts.Where(x => x.MerchantAccountId == merchantId).ToList();

                    // var transactions = new List<Transaction>();
                    var _customers = new List<CustomerDto>();
                    foreach (var item in customers)
                    {
                        var customer = new CustomerDto();
                        var transactions = _context.Transactions.Where(a => a.AccountNumber == item.AccountNumber && a.CreatedOn >= model.Date).ToList();

                        customer.AccountNumber = item.AccountNumber;
                        customer.AccountName = item.AccountName;
                        customer.Transactions = transactions.ToList();

                        _customers.Add(customer);
                    }


                    return new GetTransactionResponse
                    {
                        Customers = _customers,
                        Message = "Customer transaction records returned successfully",
                        IsSuccess = true

                    };
                }

                return new GetTransactionResponse
                {
                    Customers = null,
                    Message = "Customer transaction records failed to return",
                    IsSuccess = true

                };
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }

        }


    }



}
