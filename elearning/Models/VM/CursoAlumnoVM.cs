using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elearning.Models
{
    public class CursoAlumnoVM
    {
        public int IdCurso { get; set; }
        public string Curso1 { get; set; }
        public string Descripcion { get; set; }
        public bool PuedeAcceder { get; set; }
        
    }
}