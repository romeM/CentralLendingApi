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
    public class PersonController : ControllerBase
    {
        private IPersonService personService;
        private readonly AppSettings _appSettings;

        public PersonController(
            IPersonService personService,
            IOptions<AppSettings> appSettings)
        {
            this.personService = personService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var person = personService.Authenticate(userDto.UserName, userDto.Password);

            if (person == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRET_KEY_123456789");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, person.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            person.Token = tokenHandler.WriteToken(token);
            // return basic user info (without password) and token to store client side
            return Ok(person);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var person = HMapper.Mapper.Map<UserDto, Person>(userDto);
            try
            {
                personService.Create(person, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons = personService.GetAll();
            var userDtos = HMapper.Mapper.Map<IEnumerable<Person>,IEnumerable <UserDto>>(persons);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = personService.GetById(id);
            var userDto = HMapper.Mapper.Map<Person, UserDto>(person);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            try
            {
                this.personService.Update(userDto);
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
            personService.Delete(id);
            return Ok();
        }
    }
}
