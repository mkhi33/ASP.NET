using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManejoPresupuesto.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina = 10;
        private readonly int cantidadMaximaRecordsPorPagina = 50;

        public int RecordsPorPagina
        {
            get
            {
                return recordsPorPagina;
            }
            set 
            {
                recordsPorPagina = ( value > cantidadMaximaRecordsPorPagina ) ? cantidadMaximaRecordsPorPagina : value;
            }
        }
        public int RecordsASaltar => recordsPorPagina * (Pagina - 1);
    }
}