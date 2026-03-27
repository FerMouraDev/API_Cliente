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

        [HttpPut("{id}")] // Adicione o {id} na rota para o Axios te encontrar
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Cliente cliente)
        {
            // Garante que o ID da URL é o mesmo do objeto (Segurança de Pleno!)
            cliente.Id = id;

            var sucesso = await _clienteRepository.alterar(cliente);
            if (sucesso) return Ok("Cliente alterado com sucesso!");

            return BadRequest("Erro ao alterar cliente.");
        }

        [HttpDelete("{id}")] // Faltava este método na sua Controller!
        public async Task<ActionResult<bool>> Delete(int id)
        {
            // Criamos um objeto temporário para o Dapper entender o @Id
            var cliente = new Cliente { Id = id };

            var sucesso = await _clienteRepository.deletar(cliente);
            if (sucesso) return Ok("Cliente deletado com sucesso!");

            return BadRequest("Erro ao deletar cliente.");
        }
    }
}
