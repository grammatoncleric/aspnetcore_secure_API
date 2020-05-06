using EazyTransfer.API.Models;
using EazyTransfer.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Services
{
    public interface ICustomerService
    {
        Task<NewAccountResponse> NewAccountNumberAsync(NewAccountViewModel model);

        Task<FundAccountResponse> FundCustomerAsync(FundAccountViewModel model);
    }

    public class CustomerService : ICustomerService
    {
        private readonly EazyTransferDbContext _context;

        public CustomerService(EazyTransferDbContext context)
        {
            _context = context;
        }

        public async Task<NewAccountResponse> NewAccountNumberAsync(NewAccountViewModel model)
        {
            try
            {
                if (model == null)
                    throw new NullReferenceException("New Account Model is Null");

                // Email, Phone and ConsumerName

                //generate account number
                var AccountNumber = ApiUtil.RandomDigits();

                //check if account number already exists
                var result = _context.CustomerAccounts.FirstOrDefault(z => z.AccountNumber == AccountNumber);

                if (result == null) //if account does not exist
                {
                    //get merchant
                    var merchantId = _context.MerchantAccounts.FirstOrDefault(x => x.ApiKey == model.HashCode).MerchantAccountId;

                    if (merchantId != 0)
                    {
                        //create cust as user
                        var user = new User();
                        user.Email = model.Email;
                        user.Role = "Customer";
                        user.Password = "secret"; //not tied to aspnetUser
                        user.CreatedOn = DateTime.Now;

                       await _context.Users.AddAsync(user);
                       await _context.SaveChangesAsync();

                        //create customer with account
                        var cust = new CustomerAccount();
                        cust.MerchantAccountId = merchantId;
                        cust.AccountName = model.CustomerName;
                        cust.AccountNumber = AccountNumber;
                        cust.Phone = model.Phone;
                        cust.CreatedOn = DateTime.Now;
                        cust.Balance = 0;
                        cust.UserId = user.UserId;

                        await _context.CustomerAccounts.AddAsync(cust);
                        await _context.SaveChangesAsync();

                    }

                }
                else
                {
                    throw new NullReferenceException("New Account already exists");
                }

                return new NewAccountResponse
                {
                    AccountNumber = AccountNumber.ToString(),
                    Message = "Customer Account created successfully",
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.Message);
            }
           

        }

        public async Task<FundAccountResponse> FundCustomerAsync(FundAccountViewModel model)
        {
            try
            {
                if (model == null)
                    throw new NullReferenceException("Fund Account Model is Null");

                //Amount and Acct Numb
                //check if account number already exists
                var result = _context.CustomerAccounts.FirstOrDefault(z => z.AccountNumber == model.AccountNumber);

                if (result != null) //if account exist
                {
                    //increment Balance on CustAcct..get cust by acct num
                    var balance = _context.CustomerAccounts.FirstOrDefault(x => x.CustomerAccountId == result.CustomerAccountId).Balance;
                    balance += model.Amount;

                    result.Balance = balance;

                    _context.Entry(result).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    // _context.Update(cust.Balance);
                    //await _context.SaveChangesAsync();

                    //Enter value to Transaction table
                    var txn = new Transaction();
                    txn.Amount = model.Amount;
                    txn.Type = model.TransactionType;
                    txn.CustomerAccountId = result.CustomerAccountId.ToString();
                    txn.AccountNumber = result.AccountNumber;
                    txn.CreatedOn = DateTime.Now;

                    await _context.AddAsync(txn);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new NullReferenceException("Error Occurred.");
                }

                return new FundAccountResponse
                {
                    Message = "Account funded successfully",
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
