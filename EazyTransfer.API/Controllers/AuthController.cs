using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EazyTransfer.API.Services;
using EazyTransfer.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EazyTransfer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // /api/auth/register
        [HttpPost("Register")]
        [Route("Register")]
        public async Task<IActionResult>RegisterAsync([FromBody]RegisterViewModel model)
        {

            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400

        }

        // /api/auth/login
        [HttpPost("Login")]
        [Route("Login")]
        public async Task<IActionResult>LoginAsync([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); //Status Code : 200

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); //return code:400
        }

    }

  }