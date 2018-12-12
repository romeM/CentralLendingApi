using CentralLendingApi.Data.Models;
using MediatR;

namespace CentralLendingApi.Services.Persons.Commands.Authenticate
{
    public class AuthenticateCommand : IRequest<Person>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
