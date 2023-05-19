using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TareasMVC.Entidades
{
    public class Paso
    {
        public Guid Id { get; set; } // Permite tener Id's únicos
        public int TareaId { get; set; }
        public Tarea Tarea { get; set; }
        public string Descripcion { get; set; }
        public bool Realizado { get; set; }
        public int Orden { get; set; }
    }
}