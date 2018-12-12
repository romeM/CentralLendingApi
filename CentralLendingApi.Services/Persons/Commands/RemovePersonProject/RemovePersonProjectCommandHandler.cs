using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.DeletePersonProject
{
    public class RemovePersonProjectCommandHandler : IRequestHandler<RemovePersonProjectCommand>
    {
        private readonly CentralLendingContext context;

        public RemovePersonProjectCommandHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(RemovePersonProjectCommand request, CancellationToken cancellationToken)
        {
            PersonProject personProject = await this.context.PersonProject.FindAsync(request.Id);
            context.PersonProject.Remove(personProject);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
