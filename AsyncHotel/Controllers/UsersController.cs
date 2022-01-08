using AsyncHotel.Models.DTO;
using AsyncHotel.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _user;

        public UsersController(IUserService user)
        {
            _user = user;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> register(RegisterDTO data)
        {
            var user = await _user.Register(data, this.ModelState);
            if (ModelState.IsValid)
            {
                return Ok(user);
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login data)
        {
            var user = await _user.Login(data.UserName, data.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
    }
}
