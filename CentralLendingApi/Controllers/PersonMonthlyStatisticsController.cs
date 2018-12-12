using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.PersonMonthlyStatistics.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralLendingApi.Controllers
{
    [Authorize]
    public class PersonMonthlyStatisticsController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ICollection<PersonMonthlyStatistics>> Get(int id)
        {
            return await Mediator.Send(new GetPersonMonthlyStatisticsQuery() { PersonId = id });
        }
    }
}