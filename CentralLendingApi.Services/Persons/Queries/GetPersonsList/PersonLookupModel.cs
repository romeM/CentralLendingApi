﻿using System;

namespace CentralLendingApi.Services.Persons.Queries.GetPersonsList
{
    public class PersonLookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Note { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public int? Term { get; set; }
        public string Link { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
