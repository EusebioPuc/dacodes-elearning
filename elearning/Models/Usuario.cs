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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.AlumnoCurso = new HashSet<AlumnoCurso>();
            this.AlumnoCurso1 = new HashSet<AlumnoCurso>();
        }
    
        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int IdRol { get; set; }
        public string Token { get; set; }
        public string Estatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso1 { get; set; }
    }
}