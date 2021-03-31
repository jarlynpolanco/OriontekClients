using AutoMapper;
using Oriontek.Clients.Api.DTOs;
using Oriontek.Clients.Data.Models;

namespace Oriontek.Clients.Api.Mappings
{
    public class Maps : Profile
    {
        public Maps() 
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientCreateDTO>().ReverseMap();
            CreateMap<Client, ClientUpdateDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Address, AddressCreateDTO>().ReverseMap();
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
        }
    }
}
