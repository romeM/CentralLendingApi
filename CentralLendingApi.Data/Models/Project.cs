using System;
using System.Collections.Generic;

namespace CentralLendingApi.Data.Models
{
    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Note { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public int? Term { get; set; }
        public string Link { get; set; }
        public DateTime PollDate { get; set; }
    }
}
