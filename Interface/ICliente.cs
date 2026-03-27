using API_Cliente.Model;

namespace API_Cliente.Interface
{
    public interface ICliente
    {
        Task<bool> inserir(Cliente cliente);
        Task<bool> alterar(Cliente cliente);
        Task<bool> deletar(Cliente cliente);
        Task<List<Cliente>> buscar();
        Task<Cliente> buscarPorId(int id);
    }
}