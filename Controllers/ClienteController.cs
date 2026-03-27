using API_Cliente.Interface;
using API_Cliente.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ICliente _clienteRepository;
        public ClienteController(ICliente clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            var clientes = await _clienteRepository.buscar();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetPorId(int id)
        {
            var cliente = await _clienteRepository.buscarPorId(id);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Cliente cliente)
        {
            var sucesso = await _clienteRepository.inserir(cliente);
            if (sucesso) return Ok("Cliente inserido com sucesso!");

            return BadRequest("Erro ao inserir cliente.");
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] Cliente cliente)
        {
            var sucesso = await _clienteRepository.alterar(cliente);
            if (sucesso) return Ok("Cliente alterado com sucesso!");

            return BadRequest("Erro ao inserir cliente.");
        }
    }
}
