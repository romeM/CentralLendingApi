using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralLendingApi.Data;
using CentralLendingApi.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentralLendingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        CentralLendingApiContext centralLendingApiContext;

        public ProjectController(CentralLendingApiContext centralLendingApiContext)
        {
            this.centralLendingApiContext = centralLendingApiContext;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Project>> Get()
        {
            return this.centralLendingApiContext.Projects.ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Project> Get(int id)
        {
            return this.centralLendingApiContext.Projects.First(p => p.Id == id);
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
