using CentralLendingApi.Data.Models;
using MediatR;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonProjectsList
{
    public class GetPersonProjectsListQuery : IRequest<PersonProject[]>
    {
        public int Id { get; set; }
    }
}
