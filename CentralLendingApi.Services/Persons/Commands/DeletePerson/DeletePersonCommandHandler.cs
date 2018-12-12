using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly CentralLendingContext context;

        public DeletePersonCommandHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var user =  await this.context.Person.FindAsync(request.Id);
            if (user == null)
                throw new AppException("Utilisateur non trouvé.");
            
            this.context.Person.Remove(user);
            await this.context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
