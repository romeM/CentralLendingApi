using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralLendingApi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        CentralLendingContext centralLendingApiContext;

        public ProjectController(CentralLendingContext centralLendingApiContext)
        {
            this.centralLendingApiContext = centralLendingApiContext;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await this.centralLendingApiContext.Project.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<Project> Get(int id)
        {
            return await this.centralLendingApiContext.Project.FirstAsync(p => p.Id == id);
        }
        
        [HttpPost]
        public void Post([FromBody] Project value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Project value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
