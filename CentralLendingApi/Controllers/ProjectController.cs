using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Projects.Queries.GetProjectDetail;
using CentralLendingApi.Services.Projects.Queries.GetProjectsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ProjectLookupModel[]>> Get()
        {
            return Ok(await Mediator.Send(new GetProjectsListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDetailModel>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProjectDetailQuery { Id = id }));
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Project value)
        {
        }
    }
}
