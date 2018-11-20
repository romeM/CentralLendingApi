using System;
using System.Collections.Generic;

namespace CentralLendingApi.Data.Models
{
    public partial class UserProject
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectAmount { get; set; }
        public DateTime ProjectStartDate { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}
