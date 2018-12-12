using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Projects.Commands
{
    public class UpdateProjectStartDateCommandHandler : IRequestHandler<UpdateProjectStartDateCommand>
    {
        private readonly CentralLendingContext context;

        public UpdateProjectStartDateCommandHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateProjectStartDateCommand request, CancellationToken cancellationToken)
        {
            var project = await this.context.Project.FirstOrDefaultAsync(p => p.Id == request.ProjectId && p.StartDate == null);
            if (project != null)
            {
                project.StartDate = request.StartDate;
            }
            return Unit.Value;
        }
    }
}
