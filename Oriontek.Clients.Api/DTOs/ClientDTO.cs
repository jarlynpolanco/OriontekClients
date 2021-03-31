using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oriontek.Clients.Api.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IList<AddressDTO> Addresses { get; set; }
    }

    public class ClientCreateDTO 
    {
        [Required]
        public string DocumentNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ClientUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
