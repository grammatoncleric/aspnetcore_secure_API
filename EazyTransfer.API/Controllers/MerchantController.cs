using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EazyTransfer.API.Services;
using EazyTransfer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EazyTransfer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : Controller
    {
        private ICustomerService _customerService;
        private IMerchantService _merchantService;
        private ITransactionService _transactionService;

        public MerchantController(ICustomerService customerService, IMerchantService merchantService, ITransactionService transactionService)
        {
            _customerService = customerService;
            _merchantService = merchantService;
            _transactionService = transactionService;
        }


        // /api/merchant/newaccount

        [HttpPost("NewAccount")]
        [Route("NewAccount")]
        public async Task<IActionResult> NewAccountAsync([FromBody] NewAccountViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _customerService.NewAccountNumberAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400

        }

        // /api/merchant/fundaccount
        [HttpPost("FundAccount")]
        [Route("FundAccount")]
        public async Task<IActionResult> FundAccountAsync([FromBody] FundAccountViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _customerService.FundCustomerAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400

        }

        // /api/merchant/updatewebhook
        [HttpPost("UpdateWebHook")]
        [Route("UpdateWebHook")]
        public async Task<IActionResult> UpdateWebHookAsync([FromBody] UpdateWebHookViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _merchantService.UpdateWebHookAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400

        }

        // /api/merchant/gettransactions
        [HttpPost("GetTransactions")]
        [Route("GetTransactions")]
        public async Task<IActionResult> GetTransactionsAsync([FromBody] GetTransactionViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _transactionService.GetTransactionAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400

        }



    }
}