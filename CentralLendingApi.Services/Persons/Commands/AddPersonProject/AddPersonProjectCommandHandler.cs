using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Projects.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Persons.Commands.AddPersonProject
{
    public class AddPersonProjectCommandHandler : IRequestHandler<AddPersonProjectCommand, int>
    {
        private readonly CentralLendingContext context;
        private readonly IMediator mediator;

        public AddPersonProjectCommandHandler(CentralLendingContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<int> Handle(AddPersonProjectCommand request, CancellationToken cancellationToken)
        {
            PersonProject personProject;
            if (request.Id > 0)
            {
                personProject = await this.context.PersonProject.SingleOrDefaultAsync(pp => pp.Id == request.Id);
                personProject.Amount = request.Amount;
                personProject.StartDate = request.StartDate;
            }
            else
            {
                personProject = HMapper.Mapper.Map<AddPersonProjectCommand, PersonProject>(request);
                await this.context.PersonProject.AddAsync(personProject);
                await this.mediator.Send(new UpdateProjectStartDateCommand() { ProjectId = request.ProjectId, StartDate = request.StartDate });
            }

            await context.SaveChangesAsync();
            return personProject.Id;
        }
    }
}
