using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Services
{
    public interface IUserMonthlyStatisticsService
    {
        Task<ICollection<UserMonthlyStatistics>> GetByUserId(int user_id);
    }

    public class UserMonthlyStatisticsService : IUserMonthlyStatisticsService
    {
        private CentralLendingContext context;

        public UserMonthlyStatisticsService(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<UserMonthlyStatistics>> GetByUserId(int user_id)
        {
            return await this.context.UserMonthlyStatistics.Where(ums => ums.UserId == user_id).ToListAsync();
        }
    }
}
