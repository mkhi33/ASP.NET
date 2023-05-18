using Dapper;
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                "INSERT INTO Categorias (Nombre, TipoOperacionId, UsuarioId) VALUES (@Nombre, @TipoOperacionId, @UsuarioId); SELECT SCOPE_IDENTITY()",
                new { categoria.Nombre, categoria.TipoOperacionId, categoria.UsuarioId }
            );
            categoria.Id = id;
        }

        public async Task<IEnumerable<Categoria>> Obtener(int usuarioId, PaginacionViewModel paginacionViewModel)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categoria>(
                @$"SELECT * 
                    FROM Categorias 
                    WHERE UsuarioId = @usuarioId
                    ORDER BY Nombre
                    OFFSET {paginacionViewModel.RecordsASaltar} ROWS FETCH NEXT {paginacionViewModel.RecordsPorPagina} ROWS ONLY   
                ",
                new { usuarioId }
            );
        }
        public async Task<IEnumerable<Categoria>> Obtener(int usuarioId, TipoOperacion tipoOperacionId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categoria>(
                @"SELECT * FROM Categorias WHERE UsuarioId = @usuarioId AND TipoOperacionId = @tipoOperacionId",
                new { usuarioId, tipoOperacionId }
            );
        }

        public async Task<Categoria> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Categoria>(
                "SELECT * FROM Categorias WHERE Id = @id AND UsuarioId = @usuarioId",
                new { id, usuarioId }
            );
        }

        public async Task Actualizar(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "UPDATE Categorias SET Nombre = @Nombre, TipoOperacionId = @TipoOperacionId WHERE Id = @Id AND UsuarioId = @UsuarioId",
                new { categoria.Nombre, categoria.TipoOperacionId, categoria.Id, categoria.UsuarioId }
            );
        }

        public async Task Eliminar(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "DELETE FROM Categorias WHERE Id = @id AND UsuarioId = @usuarioId",
                new { id, usuarioId }
            );
        }

    }
}