using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, ProjectLookupModel[]>
    {
        private readonly CentralLendingContext context;

        public GetProjectsListQueryHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<ProjectLookupModel[]> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            return await this.context.Project.Select(c => HMapper.Mapper.Map<Project, ProjectLookupModel>(c)).ToArrayAsync(cancellationToken);
        }
    }
}
