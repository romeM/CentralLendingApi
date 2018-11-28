using System;
using System.Collections.Generic;

namespace CentralLendingApi.Data.Models
{
    public partial class PersonProject
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectAmount { get; set; }
        public DateTime ProjectStartDate { get; set; }

        public Person Person { get; set; }
        public Project Project { get; set; }
    }
}
