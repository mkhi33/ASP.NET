using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Interfaces
{
    public interface IRepositorioCuentas
    {
        Task Crear(Cuenta cuenta);
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task<Cuenta> ObtenerPorId(int id, int usuarioId);
        Task Actualizar(CuentaCreacionViewModel cuenta);
        Task Eliminar(int id);

    }
}