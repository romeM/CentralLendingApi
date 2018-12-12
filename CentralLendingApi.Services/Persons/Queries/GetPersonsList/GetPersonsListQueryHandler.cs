using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonsList
{
    public class GetPersonsListQueryHandler : IRequestHandler<GetPersonsListQuery, PersonLookupModel[]>
    {
        private readonly CentralLendingContext context;

        public GetPersonsListQueryHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<PersonLookupModel[]> Handle(GetPersonsListQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Person.Select(c => HMapper.Mapper.Map<Person, PersonLookupModel>(c)).ToArrayAsync(cancellationToken);
        }
    }
}
