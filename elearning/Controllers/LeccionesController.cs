using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using elearning.Models;

namespace elearning.Controllers
{
    public class LeccionesController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/Lecciones
        [Route("api/Lecciones/GetLeccion/{idUsuario}")]
        public IQueryable<Leccion> GetLeccion(int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                return db.Leccion;
            }
            else
                return null;
        }

        // GET: api/Lecciones/5
        [Route("api/Lecciones/GetLeccion/{id}/{idUsuario}")]
        [ResponseType(typeof(Leccion))]
        public IHttpActionResult GetLeccion(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Leccion leccion = db.Leccion.Find(id);
                if (leccion == null)
                {
                    return NotFound();
                }

                return Ok(leccion);
            }
            else return BadRequest();
        }

        // PUT: api/Lecciones/5
        [Route("api/Lecciones/PutLeccion/{id}/{idUsuario}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeccion(int id, int idUsuario, [FromBody]Leccion leccion)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != leccion.IdLeccion)
                {
                    return BadRequest();
                }

                db.Entry(leccion).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeccionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
            else return BadRequest();
        }

        // POST: api/Lecciones
        [Route("api/Lecciones/PostLeccion/{idUsuario}")]
        [ResponseType(typeof(Leccion))]
        public IHttpActionResult PostLeccion(int idUsuario, [FromBody]Leccion leccion)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Leccion.Add(leccion);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (LeccionExists(leccion.IdLeccion))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(new { id = leccion.IdLeccion });
                //return CreatedAtRoute("DefaultApi", new { id = leccion.IdLeccion }, leccion);
            }
            else return BadRequest();
        }

        // DELETE: api/Lecciones/5
        [Route("api/Lecciones/DeleteLeccion/{id}/{idUsuario}")]
        [ResponseType(typeof(Leccion))]
        public IHttpActionResult DeleteLeccion(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Leccion leccion = db.Leccion.Find(id);
                if (leccion == null)
                {
                    return NotFound();
                }

                db.Leccion.Remove(leccion);
                db.SaveChanges();

                return Ok(leccion);
            }
            else return BadRequest();
        }

        // GET: api/LeccionAlumno/5
        /// <summary>
        /// Obtiene lecciones para un curso, indicando a cuáles puede acceder el estudiante
        /// y detalles de la lección para responder sus preguntas.
        /// Si puede acceder es porque no ha aprobado la lección
        /// </summary>
        /// <param name="id">Id del alumno</param>
        /// <param name="idACurso">IdAlumnoCurso</param>
        /// <param name="idCurso">IdCurso</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Lecciones/LeccionAlumno/{idUsuario}/{idACurso}/{idCurso}")]
        [ResponseType(typeof(List<LeccionAlumnoVM>))]
        public IHttpActionResult LeccionAlumno(int idUsuario, int idACurso, int idCurso)
        {
            List<LeccionAlumnoVM> leccion = new List<LeccionAlumnoVM>();
            
            Usuario alumno = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 3 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (alumno != null)
            {
                //AlumnoCurso oAlumnoCursos = db.AlumnoCurso.Where(x => x.IdAlumno == alumno.IdUsuario && x.IdCurso==idACurso).FirstOrDefault();
                List<AlumnoLeccion> lsAlumnoCursoLeccion = db.AlumnoLeccion.Where(x => x.IdAlumnoCurso== idACurso).ToList();                
                List<Leccion> lsLecciones= db.Leccion.Where(x => x.IdCurso==idCurso && x.Estatus == "ACTIVO").ToList();

                if (lsLecciones.Count() > 0)
                {
                    foreach (Leccion item in lsLecciones)
                    {
                        
                        LeccionAlumnoVM ileccion = new LeccionAlumnoVM();
                        ileccion.IdLeccion= item.IdLeccion;
                        ileccion.IdCurso = item.IdCurso;
                        ileccion.Leccion1 = item.Leccion1;
                        ileccion.Descripcion = item.Descripcion;
                        ileccion.Contenido = item.Contenido;
                        ileccion.PuntajeAprobatorio = item.PuntajeAprobatorio;
                        List<AlumnoLeccion> lsRAlumnoCursosLeccion = lsAlumnoCursoLeccion.Where(x => x.IdLeccion == item.IdLeccion && x.Calificacion>=item.PuntajeAprobatorio).ToList();
                        if (lsRAlumnoCursosLeccion.Count() > 0)
                            ileccion.PuedeAcceder = false;
                        else
                            ileccion.PuedeAcceder = true;

                        leccion.Add(ileccion);
                    }
                }
                else
                    return NotFound();

                CursoAlumnoVM cursoVM = new CursoAlumnoVM();
                if (leccion == null)
                {
                    return NotFound();
                }
            }
            else
                return NotFound();




            return Ok(leccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeccionExists(int id)
        {
            return db.Leccion.Count(e => e.IdLeccion == id) > 0;
        }
    }
}