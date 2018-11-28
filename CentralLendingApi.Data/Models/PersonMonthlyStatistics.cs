using System;
using System.Collections.Generic;

namespace CentralLendingApi.Data.Models
{
    public partial class PersonMonthlyStatistics
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
        public decimal Pmt { get; set; }
        public decimal Ppmt { get; set; }
        public decimal Ipmt { get; set; }

        public Person Person { get; set; }
    }
}
