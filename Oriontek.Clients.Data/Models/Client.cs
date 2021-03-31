using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oriontek.Clients.Data.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string DocumentNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public virtual IList<Address> Addresses { get; set; }
    }
}
