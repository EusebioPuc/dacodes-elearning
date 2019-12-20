using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elearning.Models
{
    public class LeccionAlumnoVM
    {
        public int IdLeccion { get; set; }
        public string Leccion1 { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public int IdCurso { get; set; }
        public decimal PuntajeAprobatorio { get; set; }
        public bool PuedeAcceder { get; set; }
    }
}