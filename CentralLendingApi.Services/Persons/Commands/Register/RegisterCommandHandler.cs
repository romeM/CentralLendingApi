using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly CentralLendingContext context;
        private readonly IPasswordService passwordService;

        public RegisterCommandHandler(CentralLendingContext context, IPasswordService passwordService)
        {
            this.context = context;
            this.passwordService = passwordService;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // validation
            if (string.IsNullOrWhiteSpace(request.Password))
                throw new AppException("Mot de passe requis.");

            if (context.Person.Any(x => x.UserName == request.UserName))
                throw new AppException("Le login \"" + request.UserName + "\" est déjà pris.");

            byte[] passwordHash, passwordSalt;
            this.passwordService.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var person = HMapper.Mapper.Map<RegisterCommand, Person>(request);

            person.CreatedOn = DateTime.Now;
            person.UpdatedOn = DateTime.Now;
            person.PasswordHash = passwordHash;
            person.PasswordSalt = passwordSalt;

            await context.Person.AddAsync(person);
            await context.SaveChangesAsync();
            
            return Unit.Value;
        }
        
    }
}
