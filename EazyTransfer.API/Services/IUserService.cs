using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EazyTransfer.API.Models;
using EazyTransfer.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EazyTransfer.API.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        private readonly EazyTransferDbContext _context;



        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, EazyTransferDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            try
            {

                if (model == null)
                    throw new NullReferenceException("Register Model is Null");

                if (model.Password != model.ConfirmPassword)
                    return new UserManagerResponse
                    {
                        Message = "Confirm password doesn't match password",
                        IsSuccess = false,
                    };

                var IdentityUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                };

                var result = await _userManager.CreateAsync(IdentityUser, model.Password);

                if (result.Succeeded)
                {

                    //register to User and Merchant table
                    var user = new User();
                    var merchant = new MerchantAccount();
                    user.Email = model.Email;
                    user.Role = "Merchant";
                    user.Password = "secret"; //aspNetusers Management
                    user.CreatedOn = DateTime.Now;

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    merchant.UserId = user.UserId;
                    merchant.BusinessName = model.BusinessName;
                    merchant.BusinessPhone = model.BusinessPhone;
                    merchant.BusinessAddress = model.BusinessAddress;
                    merchant.ApiKey = ApiUtil.GenerateKey(); //generate random key
                    merchant.SecretKey = "secret"; //aspnet management
                    merchant.WebHook = model.MerchantWebHook;
                    merchant.CreatedOn = DateTime.Now;

                   await _context.MerchantAccounts.AddAsync(merchant);
                    await _context.SaveChangesAsync();


                    return new UserManagerResponse
                    {
                        Message = "User created successfully",
                        IsSuccess = true,

                    };
                }

                //did not succeed
                return new UserManagerResponse
                {
                    Message = "User did not create",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description),

                };

            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }
            
        }


        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return new UserManagerResponse
                    {
                        Message = "There's no user with that Email Address",
                        IsSuccess = false,

                    };
                }

                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!result)
                {
                    return new UserManagerResponse
                    {
                        Message = "Invalid Password",
                        IsSuccess = false,

                    };
                }

                var claims = new[]
                {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["AuthSettings:Issuer"],
                    audience: _configuration["AuthSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                int userid =  _context.Users.FirstOrDefault(x => x.Email == model.Email).UserId;
                string merchantAPIKey = _context.MerchantAccounts.FirstOrDefault(x => x.UserId == userid).ApiKey;

                return new UserManagerResponse
                {
                    Message = tokenAsString,
                    HashCode = merchantAPIKey,
                    IsSuccess = true,
                    ExpireDate = token.ValidTo
                };

            }
            catch (Exception ex)
            {

                throw new NullReferenceException(ex.Message);
            }
          
        }
    }

}
