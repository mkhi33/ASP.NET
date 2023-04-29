using ManejoPresupuesto.Interfaces;

namespace ManejoPresupuesto.Services
{
    public class RepositorioCategorias:IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}