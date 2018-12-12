using MediatR;

namespace CentralLendingApi.Services.Persons.Commands.DeletePersonProject
{
    public class RemovePersonProjectCommand : IRequest
    {
        public int Id { get; set; }
    }
}
