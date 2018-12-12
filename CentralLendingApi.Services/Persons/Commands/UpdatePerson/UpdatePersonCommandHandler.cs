using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly CentralLendingContext context;
        private readonly IPasswordService passwordService;

        public UpdatePersonCommandHandler(CentralLendingContext context, IPasswordService passwordService)
        {
            this.context = context;
            this.passwordService = passwordService;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await context.Person.FindAsync(request.Id);

            if (person == null)
                throw new AppException("Utilisateur non trouvé.");

            if (request.UserName != person.UserName)
            {
                // username has changed so check if the new username is already taken
                if (context.Person.Any(x => x.UserName == request.UserName))
                    throw new AppException("Le Login " + request.UserName + " est déjà pris.");
            }

            // update user properties
            HMapper.Mapper.Fill(request, person);
            person.UpdatedOn = DateTime.Now;
            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                byte[] passwordHash, passwordSalt;
                this.passwordService.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                person.PasswordHash = passwordHash;
                person.PasswordSalt = passwordSalt;
            }

            context.Person.Update(person);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
