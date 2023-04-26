using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Interfaces
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
    }
}