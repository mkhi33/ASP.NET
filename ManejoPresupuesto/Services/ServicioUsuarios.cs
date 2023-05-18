using System.Security.Claims;
using ManejoPresupuesto.Interfaces;
namespace ManejoPresupuesto.Services

{
    public class ServicioUsuarios :IServicioUsuarios
    {
        private readonly HttpContext httpContextAccessor;

        public ServicioUsuarios(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor.HttpContext;
        }
        public int ObtenerUsuarioId()
        {
            if(httpContextAccessor.User.Identity.IsAuthenticated)
            {
                var idClain = httpContextAccessor.User.Claims.Where( x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                var id = int.Parse(idClain.Value);
                return id;
            }
            else
            {
                throw new ApplicationException("El usuario no está autenticado");
            }
        }
        
    }
}