using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Interfaces
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> ObtenerTiposCuentas(int usuarioId);
        Task<TipoCuenta> ObtenerTipoCuentaPorId(int id, int usuarioId);
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Eliminar(int id);
    }
}