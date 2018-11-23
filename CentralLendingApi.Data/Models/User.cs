using System;
using System.Collections.Generic;

namespace CentralLendingApi.Data.Models
{
    public partial class User
    {
        public User()
        {
            UserMonthlyStatistics = new HashSet<UserMonthlyStatistics>();
            UserProject = new HashSet<UserProject>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string AboutMe { get; set; }
        public byte[] UserPhoto { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public ICollection<UserMonthlyStatistics> UserMonthlyStatistics { get; set; }
        public ICollection<UserProject> UserProject { get; set; }
    }
}
