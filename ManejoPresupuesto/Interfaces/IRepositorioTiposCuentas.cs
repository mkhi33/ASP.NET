using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Interfaces
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId, int id = 0);
        Task<IEnumerable<TipoCuenta>> ObtenerTiposCuentas(int usuarioId);
        Task<TipoCuenta> ObtenerTipoCuentaPorId(int id, int usuarioId);
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
        Task Ordenar( IEnumerable<TipoCuenta> tipoCuentasOrdenados);
    }
}