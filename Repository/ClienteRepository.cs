using API_Cliente.Interface;
using API_Cliente.Model;
using Dapper;
using System.Data;

namespace API_Cliente.Repository
{
    public class ClienteRepository : ICliente
    {
        private readonly IDbConnection _connection;
        public ClienteRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public async Task<bool> inserir(Cliente cliente)
        {
            var sql = @"INSERT INTO cliente (Name, Email) 
                VALUES (@Name, @Email)";
            var linhaEfetuada = await _connection.ExecuteAsync(sql, cliente);
            return linhaEfetuada > 0;
        }
        public async Task<bool> alterar(Cliente cliente)
        {
            var sql = @"UPDATE cliente 
                SET Name = @Name, 
                    Email = @Email
                WHERE Id = @Id";
            var linhaEfetuada = await _connection.ExecuteAsync(sql, cliente);
            return linhaEfetuada > 0;
        }
        public async Task<bool> deletar(Cliente cliente)
        {
            var sql = "DELETE FROM cliente WHERE Id = @Id";
            var linhaEfetuada = await _connection.ExecuteAsync(sql, cliente);
            return linhaEfetuada > 0;
        }
        public async Task<List<Cliente>> buscar()
        {
            var sql = "SELECT * FROM cliente";
            var resultado = await _connection.QueryAsync<Cliente>(sql);
            return resultado.ToList();
        }

        public async Task<Cliente?> buscarPorId(int id)
        {
            var sql = "SELECT * FROM cliente WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { id });
        }
    }
}
