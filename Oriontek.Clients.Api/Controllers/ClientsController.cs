using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Oriontek.Clients.Api.DTOs;
using Oriontek.Clients.Contract;
using Oriontek.Clients.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oriontek.Clients.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientCreateDTO clientDTO)
        {
            var location = GetCotrollerActionNames();
            try
            {
                if (clientDTO == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = _mapper.Map<Client>(clientDTO);
                await _clientRepository.Create(client);

                return Created("Create", new { client });
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientUpdateDTO clientDTO)
        {
            var location = GetCotrollerActionNames();
            try
            {
                if (id < 1 || clientDTO == null || id != clientDTO.Id)
                    return BadRequest();

                var isExist = await _clientRepository.IsExist(id);
                if (!isExist)
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var client = _mapper.Map<Client>(clientDTO);
                await _clientRepository.Update(client);

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

                var isExist = await _clientRepository.IsExist(id);
                if (!isExist)
                    return NotFound();

                var client = await _clientRepository.FindById(id);
                await _clientRepository.Delete(client);

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var location = GetCotrollerActionNames();
            try
            {
                var clients = await _clientRepository.FindAll();
                var response = _mapper.Map<IList<ClientDTO>>(clients);

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalServerError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetClient(int clientId)
        {
            var location = GetCotrollerActionNames();
            try
            {
                var client = await _clientRepository.FindById(clientId);
                var response = _mapper.Map<ClientDTO>(client);

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
