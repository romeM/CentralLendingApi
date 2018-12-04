using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IProjectService projectService;

        public ProjectController(CentralLendingContext centralLendingApiContext, IProjectService projectService)
        {
            this.projectService = projectService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await this.projectService.Get();
        }

        [HttpGet("suggest/{term}")]
        public async Task<IEnumerable<Project>> Suggest(string term)
        {
            return await this.projectService.Suggest(term);
        }

        [HttpGet("person")]
        public async Task<IEnumerable<PersonProjectDto>> GetPersonProjects()
        {
            return await this.projectService.GetPersonProjects();
        }

        [HttpGet("{id}")]
        public async Task<Project> Get(int id)
        {
            return await this.projectService.Get(id);
        }
        
        [HttpPost]
        public async Task Post([FromBody] PersonProjectDto personProjectDto)
        {
            await this.projectService.AddPersonToProject(personProjectDto);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProjectDto value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
