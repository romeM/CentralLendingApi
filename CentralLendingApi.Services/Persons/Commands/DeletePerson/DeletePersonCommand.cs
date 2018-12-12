using MediatR;
using System;

namespace CentralLendingApi.Services.Persons.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest
    {
        public int Id { get; set; }
    }
}
