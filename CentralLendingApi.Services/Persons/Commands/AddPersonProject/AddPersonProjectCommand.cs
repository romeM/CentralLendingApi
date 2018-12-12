using MediatR;
using System;

namespace CentralLendingApi.Services.Persons.Commands.AddPersonProject
{
    public class AddPersonProjectCommand: IRequest<int>
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
    }
}
