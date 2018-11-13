using CentralLendingApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralLendingApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CentralLendingApiContext context)
        {
            context.Database.EnsureCreated();

            if (context.Projects.Any())
            {
                return;   // DB has been seeded
            }

            var projects = new Project[]
            {
            new Project{Name="KA MATE STRATEGY",Platform="creditfr",Note= "C",Amount=52000, Rate=7.90, Term=36, Link="https://www.credit.fr/crowdfunding/ka-mate-strategy-developpement-d-une-entreprise-de-conseil-a-l-etranger", PollDate=DateTime.Parse("2018-11-12")},
            new Project{Name="BLERIOT CONDUITE",Platform="creditfr",Note= "C",Amount=31000, Rate=7.90, Term=36, Link="https://www.credit.fr/crowdfunding/bleriot-conduite-actions-commerciales-et-marketing-pour-une-ecole-de-conduite", PollDate=DateTime.Parse("2018-11-12")},
            new Project{Name="TRIPARTITE",Platform="creditfr",Note= "B+",Amount=500000, Rate=4.30, Term=12, Link="https://www.credit.fr/crowdfunding/tripartite-achat-de-materiel-pour-une-entreprise-specialisee-en-maintenance-de-batiments-et-materiels-militaires", PollDate=DateTime.Parse("2018-11-12")},
            };
            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }
            context.SaveChanges();
        }
    }
}
