using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using CentralLendingApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CentralLendingApi.Services;
using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Services.Services;
using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Helpers;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            this.userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = userService.Authenticate(userDto.UserName, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRET_KEY_123456789");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // return basic user info (without password) and token to store client side
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = HMapper.Mapper.Map<UserDto, User>(userDto);

            try
            {
                // save 
                user.CreatedOn = DateTime.Now;
                user.UpdatedOn = DateTime.Now;
                userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            var userDtos = HMapper.Mapper.Map<IEnumerable<User>,IEnumerable <UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = userService.GetById(id);
            var userDto = HMapper.Mapper.Map<User, UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            try
            {
                this.userService.Update(userDto);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            return Ok();
        }
    }
}
