using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CentralLendingApi.Data.Models
{
    public partial class User
    {
        [NotMapped]
        public string Token { get; set; }
    }
}
