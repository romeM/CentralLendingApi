using System;

namespace CentralLendingApi.Data.Dtos
{
    public class PersonProjectDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public ProjectDto Project { get; set; }
    }
}
