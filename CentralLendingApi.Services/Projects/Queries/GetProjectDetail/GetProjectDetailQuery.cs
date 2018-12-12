using MediatR;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectDetail
{
    public class GetProjectDetailQuery : IRequest<ProjectDetailModel>
    {
        public int Id { get; set; }
    }
}
