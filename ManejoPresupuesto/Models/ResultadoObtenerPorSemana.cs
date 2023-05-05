using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class ResultadoObtenerPorSemana
    {
        public int Semana { get; set; }
        public decimal Monto { get; set; }
        public TipoOperacion TipoOperacionId { get; set; }

    }
}