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
    public class RespuestasController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/Respuestas
        [Route("api/Respuestas/GetRespuesta/{idUsuario}")]
        public IQueryable<Respuesta> GetRespuesta(int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                return db.Respuesta;
            }
            else
                return null;
        }

        // GET: api/Respuestas/5
        [Route("api/Respuestas/GetRespuesta/{id}/{idUsuario}")]
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult GetRespuesta(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Respuesta respuesta = db.Respuesta.Find(id);
                if (respuesta == null)
                {
                    return NotFound();
                }

                return Ok(respuesta);
            }
            else return BadRequest();
        }

        // PUT: api/Respuestas/5
        [Route("api/Respuestas/PutRespuesta/{id}/{idUsuario}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRespuesta(int id, int idUsuario, [FromBody]Respuesta respuesta)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != respuesta.IdRespuesta)
                {
                    return BadRequest();
                }

                db.Entry(respuesta).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestaExists(id))
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

        // POST: api/Respuestas
        [Route("api/Respuestas/PostRespuesta/{idUsuario}")]
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult PostRespuesta(int idUsuario, [FromBody]Respuesta respuesta)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Respuesta.Add(respuesta);
                db.SaveChanges();

                return Ok(new { id = respuesta.IdRespuesta });
                //return CreatedAtRoute("DefaultApi", new { id = respuesta.IdRespuesta }, respuesta);
            }
            else return BadRequest();
        }

        // DELETE: api/Respuestas/5
        [Route("api/Respuestas/DeleteRespuesta/{id}/{idUsuario}")]
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult DeleteRespuesta(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Respuesta respuesta = db.Respuesta.Find(id);
                if (respuesta == null)
                {
                    return NotFound();
                }

                db.Respuesta.Remove(respuesta);
                db.SaveChanges();

                return Ok(respuesta);
            }
            else return BadRequest();
        }

        /// <summary>
        /// De una lección, para evitar varias solicitudes, se envian todas las respuestas de una vez
        /// No se calificará la respuesta porque implica otro proceso, solamente se almacenará
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IdALeccion">IdAlumnoLeccion</param>
        /// <param name="Respuestas">Listado de respuestas seleccionadas por el alumno</param>
        /// <returns></returns>
        // POST: api/Respuestas
        [HttpPost]
        [Route("api/Respuestas/RespuestasLeccionAlumno/{idUsuario}/{idALeccion}")]
        [ResponseType(typeof(Boolean))]
        public IHttpActionResult RespuestasLeccionAlumno(int idUsuario, int idALeccion, [FromBody] List<PreguntaAlumnoVM> Respuestas)
        {
            Usuario alumno = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 3 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (alumno != null)
            {
                AlumnoLeccion oAlumnoCursoLeccion = db.AlumnoLeccion.Where(x => x.IdAlumnoLeccion == idALeccion).FirstOrDefault();
                if (oAlumnoCursoLeccion!=null)
                {
                    foreach (var item in Respuestas)
                    {
                        AlumnoPregunta oAluPregunta = new AlumnoPregunta();                        
                        oAluPregunta.IdAlumnoLeccion =item.IdAlumnoLeccion;
                        oAluPregunta.IdPregunta =item.IdPregunta;
                        oAluPregunta.IdRespuesta =item.IdRespuesta;
                        oAluPregunta.Puntos =item.Puntos;                        

                        db.AlumnoPregunta.Add(oAluPregunta);
                        db.SaveChanges();
                    }
                }
            }
            else
                return BadRequest();
            
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RespuestaExists(int id)
        {
            return db.Respuesta.Count(e => e.IdRespuesta == id) > 0;
        }
    }
}