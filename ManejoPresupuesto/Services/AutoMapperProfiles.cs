
using AutoMapper;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Cuenta, CuentaCreacionViewModel>();
            CreateMap<TransaccionCreacionViewModel, Transaccion>().ReverseMap();
        }
    }
}