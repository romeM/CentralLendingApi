using CentralLendingApi.Data.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonDetail
{
    public class GetPersonDetailQueryHandler: IRequestHandler<GetPersonDetailQuery, PersonDetailModel>
    {
        private readonly CentralLendingContext context;

        public GetPersonDetailQueryHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<PersonDetailModel> Handle(GetPersonDetailQuery request, CancellationToken cancellationToken)
        {
            return HMapper.Mapper.Map<Person, PersonDetailModel>(await this.context.Person.FindAsync(request.Id));
        }
    }
}
