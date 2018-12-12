using CentralLendingApi.Services.Exceptions;
using CentralLendingApi.Services.Persons.Commands.Authenticate;
using CentralLendingApi.Services.Persons.Commands.Register;
using CentralLendingApi.Services.Persons.Commands.UpdatePerson;
using CentralLendingApi.Services.Persons.Commands.AddPersonProject;
using CentralLendingApi.Services.Persons.Queries.GetPersonDetail;
using CentralLendingApi.Services.Persons.Queries.GetPersonsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CentralLendingApi.Services.Persons.Commands.DeletePerson;
using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Persons.Queries.GetPersonProjectsList;
using CentralLendingApi.Services.Persons.Commands.DeletePersonProject;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    public class PersonController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateCommand authenticateCommand)
        {
            try
            {
                return Ok(await Mediator.Send(authenticateCommand));
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand registerCommand)
        {
            try
            {
                await Mediator.Send(registerCommand);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personLookupModels = await Mediator.Send(new GetPersonsListQuery());
            return Ok(personLookupModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PersonDetailModel personDetailModel = await Mediator.Send(new GetPersonDetailQuery() { Id = id });
            return Ok(personDetailModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePersonCommand updatePersonCommand)
        {
            try
            {
                await Mediator.Send(updatePersonCommand);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePersonCommand() { Id = id });
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("project")]
        public async Task<int> Post([FromBody] AddPersonProjectCommand addPersonProjectCommand)
        {
            return await Mediator.Send(addPersonProjectCommand);
        }
        
        [HttpGet("projects")]
        public async Task<ActionResult<PersonProject[]>> GetPersonProjects()
        {
            return Ok(await Mediator.Send(new GetPersonProjectsListQuery() { Id = int.Parse(HttpContext.User.Identity.Name) }));
        }

        [HttpDelete("project/{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await Mediator.Send(new RemovePersonProjectCommand { Id = id });
            return Ok();
        }
    }
}
