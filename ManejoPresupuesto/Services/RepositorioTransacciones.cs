using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ManejoPresupuesto.Interfaces;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public class RepositorioTransacciones : IRepositorioTransacciones
    {
        private readonly string connectionString;
        public RepositorioTransacciones(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(Transaccion transaccion)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Transacciones_Insertar", new
            {
                transaccion.UsuarioId,
                transaccion.FechaTransaccion,
                transaccion.Monto,
                transaccion.CategoriaId,
                transaccion.CuentaId,
                transaccion.Nota
            },
            commandType: System.Data.CommandType.StoredProcedure);
            transaccion.Id = id;
        }
        public async Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>(
                @"
                    SELECT 
                        t.Id,
                        t.Monto,
                        t.FechaTransaccion,
                        C.Nombre AS Categoria,
                        cu.Nombre AS Cuenta,
                        c.TipoOperacionId 
                    FROM Transacciones t 
                        INNER JOIN Categorias c
                            ON c.Id = t.CategoriaId 
                        INNER JOIN Cuentas cu 
                            ON cu.Id = t.CuentaId
                    WHERE 
                        t.CuentaId = @CuentaId  AND 
                        t.UsuarioId = @UsuarioId AND 
                        FechaTransaccion BETWEEN @FechaInicio AND @FechaFin;
                ",
                modelo
            );
        }
        public async Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>(
                @"
                    SELECT 
                        t.Id,
                        t.Monto,
                        t.FechaTransaccion,
                        C.Nombre AS Categoria,
                        cu.Nombre AS Cuenta,
                        c.TipoOperacionId 
                    FROM Transacciones t 
                        INNER JOIN Categorias c
                            ON c.Id = t.CategoriaId 
                        INNER JOIN Cuentas cu 
                            ON cu.Id = t.CuentaId
                    WHERE 
                        t.UsuarioId = @UsuarioId AND 
                        FechaTransaccion BETWEEN @FechaInicio AND @FechaFin
                    ORDER BY t.FechaTransaccion DESC;
                ",
                modelo
            );
        }

        public async Task Actualizar(Transaccion transaccion, decimal montoAnterior, int cuentaAnteriorId)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("Transacciones_Actualizar",
            new
            {
                transaccion.Id,
                transaccion.FechaTransaccion,
                transaccion.Monto,
                transaccion.CategoriaId,
                transaccion.CuentaId,
                transaccion.Nota,
                montoAnterior,
                cuentaAnteriorId
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Transaccion> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaccion>(
                @"SELECT Transacciones.*, cat.TipoOperacionId FROM Transacciones
                    INNER JOIN Categorias cat 
                        ON cat.Id = Transacciones.CategoriaId 
                WHERE Transacciones.Id = @Id AND Transacciones.UsuarioId  = @UsuarioId;",
                new { id, usuarioId }
            );
        }

        public async Task<IEnumerable<ResultadoObtenerPorSemana>> ObtenerPorSemana(ParametroObtenerTransaccionesPorUsuario modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<ResultadoObtenerPorSemana>(
                @"
                    SELECT 
                        DATEDIF(d, @fechaInicio, FechaTransaccion)/7 + 1 AS Semana,
                        SUM(Monto) AS Monto,
                        cat.TipoOperacionId
                    FROM Transacciones
                    INNER JOIN Categorias cat 
                        ON cat.Id = Transacciones.CategoriaId
                    WHERE 
                        Transacciones.UsuarioId = @usuarioId AND 
                        FechaTransaccion BETWEEN @fechaInicio AND @fechaFin
                    GROUP BY DATEDIF(d, @fechaInicio, FechaTransaccion) / 7, cat.TipoOperacionId;
                ",
                modelo
            );
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("Transacciones_Borrar", new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}