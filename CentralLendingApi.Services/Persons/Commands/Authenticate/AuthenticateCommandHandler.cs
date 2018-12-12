using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Person>
    {
        private readonly CentralLendingContext context;
        private readonly IPasswordService passwordService;

        public AuthenticateCommandHandler(CentralLendingContext context, IPasswordService passwordService)
        {
            this.context = context;
            this.passwordService = passwordService;
        }

        public async Task<Person> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var person = await this.Authenticate(request.UserName, request.Password);

            if (person == null)
                throw new AppException("Le login ou le mot de passe est incorrect.");

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
            return person;
            
        }
        private async Task<Person> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var person = await context.Person.SingleOrDefaultAsync(x => x.UserName == username);

            // check if username exists
            if (person == null)
                return null;

            // check if password is correct
            if (!this.passwordService.VerifyPasswordHash(password, person.PasswordHash, person.PasswordSalt))
                return null;

            // authentication successful
            return person;
        }
    }
}
