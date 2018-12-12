using MediatR;

namespace CentralLendingApi.Services.PersonMonthlyStatistics.Queries
{
    public class GetPersonMonthlyStatisticsQuery : IRequest<Data.Models.PersonMonthlyStatistics[]>
    {
        public int PersonId;
    }
}
