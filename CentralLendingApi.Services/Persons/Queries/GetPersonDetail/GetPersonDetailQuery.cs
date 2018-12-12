using MediatR;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonDetail
{
    public class GetPersonDetailQuery: IRequest<PersonDetailModel>
    {
        public int Id;
    }
}
