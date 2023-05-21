using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TareasMVC.Models
{
    public class UsuariosListadoViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }
        public string Mensaje { get; set; }
    }
}