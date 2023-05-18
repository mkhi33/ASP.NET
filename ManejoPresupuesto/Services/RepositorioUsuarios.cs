using Dapper;
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly string connectionString;

        public RepositorioUsuarios(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            usuario.EmailNormalizado = usuario.Email.ToUpper();
            using var connection = new SqlConnection(this.connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Usuarios (Email, EmailNormalizado, PasswordHash) VALUES (@Email, @EmailNormalizado, @PasswordHash);
                SELECT SCOPE_IDENTITY();
                ",
                usuario
            );
            return id;
        }

        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(this.connectionString);
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(
                @"SELECT * FROM Usuarios WHERE EmailNormalizado = @EmailNormalizado;",
                new { EmailNormalizado = emailNormalizado }
            );
            return usuario;
        }
    }
}