using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Persons.Commands.AddPersonProject;
using CentralLendingApi.Services.Persons.Commands.UpdatePerson;
using CentralLendingApi.Services.Persons.Queries.GetPersonDetail;
using CentralLendingApi.Services.Persons.Queries.GetPersonsList;
using CentralLendingApi.Services.Projects.Queries.GetProjectDetail;
using CentralLendingApi.Services.Projects.Queries.GetProjectsList;
using HMapper;
namespace CentralLendingApi.Services.Mappers
{
    public static class InitMapperConfiguration
    {
        public static void Init(IMapperAPIInitializer initializer)
        {
            initializer.Map<UpdatePersonCommand, Person>();
            initializer.Map<Project, ProjectDetailModel>();
            initializer.Map<Project, ProjectLookupModel>();
            initializer.Map<Person, PersonLookupModel>();
            initializer.Map<Person, PersonDetailModel>();
            initializer.Map<AddPersonProjectCommand, PersonProject>();
        }
    }
}
