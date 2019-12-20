using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elearning.Models
{
    public class PreguntaAlumnoVM
    {
        public int IdAlumnoPregunta { get; set; }
        public int IdAlumnoLeccion { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }
        public decimal Puntos { get; set; }
    }
}