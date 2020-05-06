using EazyTransfer.API.Models;
using EazyTransfer.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EazyTransfer.API.Services
{
    public interface IMerchantService
    {
        Task<UpdateWebHookResponse> UpdateWebHookAsync(UpdateWebHookViewModel model);
    }

    public class MerchantService : IMerchantService
    {
        private readonly EazyTransferDbContext _context;

        public MerchantService(EazyTransferDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateWebHookResponse> UpdateWebHookAsync(UpdateWebHookViewModel model)
        {
            try
            {
                //get merchant
                var merchant = _context.MerchantAccounts.FirstOrDefault(x => x.ApiKey == model.HashCode);

                if (merchant != null)
                {
                    //update merchant webhook...
                    merchant.WebHook = model.WebHookURL;

                    _context.Entry(merchant).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return new UpdateWebHookResponse
                {
                    Message = "Merchant Webhook updated successfully",
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
