using System.ComponentModel.DataAnnotations.Schema;

namespace CentralLendingApi.Data.Models
{
    public partial class Person
    {
        [NotMapped]
        public string Token { get; set; }
    }
}
