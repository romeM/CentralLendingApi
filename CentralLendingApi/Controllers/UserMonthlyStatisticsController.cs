using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentralLendingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMonthlyStatisticsController : ControllerBase
    {
        private IUserMonthlyStatisticsService userMonthlyStatisticsService;

        public UserMonthlyStatisticsController(
            IUserMonthlyStatisticsService userMonthlyStatisticsService)
        {
            this.userMonthlyStatisticsService = userMonthlyStatisticsService;
        }

        [HttpGet("{id}")]
        public async Task<ICollection<UserMonthlyStatistics>> Get(int id)
        {
            return await this.userMonthlyStatisticsService.GetByUserId(id);
        }
    }
}