using MediatR;
using System;

namespace CentralLendingApi.Services.Projects.Commands
{
    public class UpdateProjectStartDateCommand : IRequest
    {
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
