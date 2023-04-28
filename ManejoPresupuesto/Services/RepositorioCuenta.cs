
using Dapper;
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public class RepositorioCuentas: IRepositorioCuentas
    {
        private readonly string connectionString;

        public RepositorioCuentas( IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta){
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Cuentas (Nombre, TipoCuentaId, Descripcion, Balance) VALUES (@Nombre, @TipoCuentaId, @Descripcion, @Balance);
                SELECT SCOPE_IDENTITY()", cuenta);
            cuenta.Id = id;

        }
    }
}