using AsyncHotel.Models.DTO;
using AsyncHotel.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> _context;

        public IdentityUserService(UserManager<ApplicationUser> context)
        {
            _context = context;
        }
        public async Task<UserDto> Login(string userName, string password)
        {
            var user = await _context.FindByNameAsync(userName);
            if(await _context.CheckPasswordAsync(user, password))
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
            }

            return null;
        }

        public async Task<UserDto> Register(RegisterDTO data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
            };
            var result = await _context.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };
            }

            foreach (var error in result.Errors)
            {
                var _key = error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    error.Code.Contains("Email") ? nameof(data.Email) : "";

                modelState.AddModelError(_key, error.Description);
            }
            return null;
        }
    }
}
