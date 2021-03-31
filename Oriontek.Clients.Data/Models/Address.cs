using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oriontek.Clients.Data.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Street { get; set; }

        public string Section { get; set; }

        public string Province { get; set; }

        public string Municipality { get; set; }

        public string District { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

    }
}
