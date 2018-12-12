using CentralLendingApi.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.PersonMonthlyStatistics.Queries
{
    public class GetPersonMonthlyStatisticsQueryHandler : IRequestHandler<GetPersonMonthlyStatisticsQuery, Data.Models.PersonMonthlyStatistics[]>
    {
        private readonly CentralLendingContext context;

        public GetPersonMonthlyStatisticsQueryHandler(CentralLendingContext context)
        {
            this.context = context;
        }

        public async Task<Data.Models.PersonMonthlyStatistics[]> Handle(GetPersonMonthlyStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await this.context.PersonMonthlyStatistics.Where(ums => ums.PersonId == request.PersonId).OrderBy(ums => ums.Date).ToArrayAsync();
        }
    }
}
