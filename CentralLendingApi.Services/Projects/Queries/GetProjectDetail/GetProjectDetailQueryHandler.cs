using System.Threading;
using System.Threading.Tasks;
using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Exceptions;
using MediatR;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectDetail
{
    public class GetProjectDetailQueryHandler : IRequestHandler<GetProjectDetailQuery, ProjectDetailModel>
    {
        private CentralLendingContext _context;

        public GetProjectDetailQueryHandler(CentralLendingContext context)
        {
            _context = context;
        }

        public async Task<ProjectDetailModel> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Project
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Project), request.Id);
            }

            return HMapper.Mapper.Map<Project, ProjectDetailModel>(entity);
        }
    }
}
