using AsyncHotel.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterDTO data, ModelStateDictionary modelState);
        public Task<UserDto> Login(string UserName, string Password);
    }
}
