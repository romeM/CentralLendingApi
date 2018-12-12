using MediatR;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQuery : IRequest<ProjectLookupModel[]>
    {
    }
}
