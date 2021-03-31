using System.ComponentModel.DataAnnotations;

namespace Oriontek.Clients.Api.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Section { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string District { get; set; }
        public ClientDTO Client { get; set; }
    }

    public class AddressCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClientId { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string District { get; set; }
    }

    public class AddressUpdateDTO 
    {
        [Required]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Section { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string District { get; set; }
    }
}
