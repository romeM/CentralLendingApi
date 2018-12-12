using FluentValidation;

namespace CentralLendingApi.Services.Projects.Queries.GetProjectDetail
{
    public class GetProjectDetailQueryValidator : AbstractValidator<GetProjectDetailQuery>
    {
        public GetProjectDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
