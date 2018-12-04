using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Data.Models;
using HMapper;
namespace CentralLendingApi.Services.Mappers
{
    public static class InitMapperConfiguration
    {
        public static void Init(IMapperAPIInitializer initializer)
        {
            initializer.Map<Person, PersonDto>();
            initializer.Map<PersonDto, Person>();
            initializer.Map<ProjectDto, Project>();
            initializer.Map<Project, ProjectDto>();

            initializer.Map<PersonProject, PersonProjectDto>();
            initializer.Map<PersonProjectDto, PersonProject>()
                .WithMember(x => x.Person, pp => pp.Ignore())
                .WithMember(x => x.Project, pp => pp.Ignore());
        }
    }
}
