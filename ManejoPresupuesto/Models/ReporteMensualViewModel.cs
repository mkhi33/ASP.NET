using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class ReporteMensualViewModel
    {
        public IEnumerable<ResultadoObtenerPorMes> TransaccionesPorMes { get; set; }
        public decimal Ingresos => TransaccionesPorMes.Sum(t => t.Ingreso);
        public decimal Gastos => TransaccionesPorMes.Sum(t => t.Gasto);
        public decimal Total => Ingresos - Gastos;
        public int Año { get; set; }
        
    }
}