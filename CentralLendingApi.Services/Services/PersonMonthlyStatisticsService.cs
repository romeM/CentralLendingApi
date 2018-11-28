using CentralLendingApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Services
{
    public interface IPersonMonthlyStatisticsService
    {
        Task<ICollection<PersonMonthlyStatistics>> GetByUserId(int user_id);
    }

    public class PersonMonthlyStatisticsService : IPersonMonthlyStatisticsService
    {
        private CentralLendingContext context;

        public PersonMonthlyStatisticsService(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<PersonMonthlyStatistics>> GetByUserId(int user_id)
        {
            return await this.context.PersonMonthlyStatistics.Where(ums => ums.PersonId == user_id).OrderBy(ums => ums.Date).ToListAsync();
        }
    }
}
