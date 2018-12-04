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

        public ProjectController(IProjectService projectService)
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
        public async Task<int> Post([FromBody] PersonProjectDto personProjectDto)
        {
            return await this.projectService.AddPersonProject(personProjectDto);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProjectDto value)
        {
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this.projectService.DeletePersonProject(id);
        }
    }
}
