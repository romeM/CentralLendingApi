using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralLendingApi.Services.Persons.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string AboutMe { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Image { get; set; }
    }
}
