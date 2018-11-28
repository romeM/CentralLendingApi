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
    public class PersonMonthlyStatisticsController : ControllerBase
    {
        private IPersonMonthlyStatisticsService personMonthlyStatisticsService;

        public PersonMonthlyStatisticsController(
            IPersonMonthlyStatisticsService personMonthlyStatisticsService)
        {
            this.personMonthlyStatisticsService = personMonthlyStatisticsService;
        }

        [HttpGet("{id}")]
        public async Task<ICollection<PersonMonthlyStatistics>> Get(int id)
        {
            return await this.personMonthlyStatisticsService.GetByUserId(id);
        }
    }
}