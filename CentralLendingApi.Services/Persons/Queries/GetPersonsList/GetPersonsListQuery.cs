using MediatR;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonsList
{
    public class GetPersonsListQuery : IRequest<PersonLookupModel[]>
    {
    }
}
