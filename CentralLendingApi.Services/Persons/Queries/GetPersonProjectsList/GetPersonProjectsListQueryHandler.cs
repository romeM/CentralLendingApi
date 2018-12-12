using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonProjectsList
{
    public class GetPersonProjectsListQueryHandler : IRequestHandler<GetPersonProjectsListQuery, PersonProject[]>
    {
        private readonly CentralLendingContext context;

        public GetPersonProjectsListQueryHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<PersonProject[]> Handle(GetPersonProjectsListQuery request, CancellationToken cancellationToken)
        {
            return await this.context.PersonProject.Include(pp => pp.Project).Where(pp => pp.PersonId == request.Id).ToArrayAsync();
        }
    }
}
