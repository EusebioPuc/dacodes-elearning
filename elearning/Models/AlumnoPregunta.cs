//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace elearning.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AlumnoPregunta
    {
        public int IdAlumnoPregunta { get; set; }
        public int IdAlumnoLeccion { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }
        public decimal Puntos { get; set; }
        public Nullable<bool> Correcto { get; set; }
    
        public virtual AlumnoLeccion AlumnoLeccion { get; set; }
        public virtual Respuesta Respuesta { get; set; }
        public virtual Pregunta Pregunta { get; set; }
    }
}