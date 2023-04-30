
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Interfaces
{
    public interface IRepositorioTransacciones
    {
        Task Crear(Transaccion transaccion);
    }
}