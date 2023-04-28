
using Dapper;
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden) 
                                                    VALUES (@Nombre, @UsuarioId, 0);
                                                    SELECT SCOPE_IDENTITY();", tipoCuenta);
            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM TiposCuentas WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;", new { nombre, usuarioId });
            return existe == 1;
        }
        public async Task<IEnumerable<TipoCuenta>> ObtenerTiposCuentas(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT * FROM TiposCuentas WHERE UsuarioId = @usuarioId ORDER BY Orden;", new { usuarioId });
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas SET Nombre = @Nombre WHERE Id = @Id;", tipoCuenta);
        }
        public async Task<TipoCuenta> ObtenerTipoCuentaPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT * FROM TiposCuentas WHERE Id = @id AND UsuarioId = @usuarioId;", new { id, usuarioId });
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM TiposCuentas WHERE Id = @id", new { id });
        }

        public async Task Ordenar( IEnumerable<TipoCuenta> tipoCuentasOrdenados)
        {
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }

    }
}