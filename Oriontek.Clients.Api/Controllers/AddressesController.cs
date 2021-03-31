using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oriontek.Clients.Api.DTOs;
using Oriontek.Clients.Contract;
using Oriontek.Clients.Contracts;
using Oriontek.Clients.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oriontek.Clients.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressesController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AddressesController(IAddressRepository addressRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressCreateDTO addressDTO)
        {
            var location = GetCotrollerActionNames();
            try
            {
                if (addressDTO == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var address = _mapper.Map<Address>(addressDTO);

                var exists = await _clientRepository.IsExist(addressDTO.ClientId);

                if(!exists)
                    return BadRequest("Client not found!");

                await _addressRepository.Create(address);

                return Created("Create", new { address });
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddressUpdateDTO addressDTO)
        {
            var location = GetCotrollerActionNames();
            try
            {
                if (id < 1 || addressDTO == null || id != addressDTO.Id)
                    return BadRequest();

                var isExist = await _addressRepository.IsExist(id);
                if (!isExist)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var address = _mapper.Map<Address>(addressDTO);
                await _addressRepository.Update(address);

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = GetCotrollerActionNames();
            try
            {
                if (id < 1)
                    return BadRequest();

                var isExist = await _addressRepository.IsExist(id);
                if (!isExist)
                    return NotFound();

                var address = await _addressRepository.FindById(id);
                await _addressRepository.Delete(address);

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var location = GetCotrollerActionNames();
            try
            {
                var addresses = await _addressRepository.FindAll();
                var response = _mapper.Map<IList<AddressDTO>>(addresses);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpGet("{addressId:int}")]
        public async Task<IActionResult> GetAddresById(int addressId)
        {
            var location = GetCotrollerActionNames();
            try
            {
                var address = await _addressRepository.FindById(addressId);
                var response = _mapper.Map<AddressDTO>(address);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetAddresByClientId(int clientId)
        {
            var location = GetCotrollerActionNames();
            try
            {
                var address = await _clientRepository.FindById(clientId);
                var response = _mapper.Map<IList<AddressDTO>>(address.Addresses);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        private string GetCotrollerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} - {action}";
        }

        private ObjectResult InternalServerError(string message)
        {
            return StatusCode(500, "Something went wrong. Please contact the System Administrator.");
        }
    }

}
