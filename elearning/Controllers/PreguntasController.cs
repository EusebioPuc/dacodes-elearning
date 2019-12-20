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
    public class PreguntasController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/Preguntas
        [Route("api/Preguntas/GetPregunta/{idUsuario}")]
        public IQueryable<Pregunta> GetPregunta(int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                return db.Pregunta;
            }
            else
                return null;
        }

        // GET: api/Preguntas/5
        [Route("api/Preguntas/GetPregunta/{id}/{idUsuario}")]
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult GetPregunta(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Pregunta pregunta = db.Pregunta.Find(id);
                if (pregunta == null)
                {
                    return NotFound();
                }

                return Ok(pregunta);
            }
            else return BadRequest();
        }

        // PUT: api/Preguntas/5
        [Route("api/Preguntas/PutPregunta/{id}/{idUsuario}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPregunta(int id, int idUsuario, [FromBody]Pregunta pregunta)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != pregunta.IdPregunta)
                {
                    return BadRequest();
                }

                db.Entry(pregunta).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntaExists(id))
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

        // POST: api/Preguntas
        [Route("api/Preguntas/PostPregunta/{idUsuario}")]
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult PostPregunta(int idUsuario, [FromBody]Pregunta pregunta)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Pregunta.Add(pregunta);
                db.SaveChanges();

                return Ok(new { id = pregunta.IdPregunta });
                //return CreatedAtRoute("DefaultApi", new { id = pregunta.IdPregunta }, pregunta);
            }
            else return BadRequest();
        }

        // DELETE: api/Preguntas/5
        [Route("api/Preguntas/DeletePregunta/{id}/{idUsuario}")]
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult DeletePregunta(int id, int idUsuario)
        {
            Usuario maestro = db.Usuario.Where(x => x.IdUsuario == idUsuario && x.IdRol == 2 && x.Estatus == "ACTIVO").FirstOrDefault();
            if (maestro != null)
            {
                Pregunta pregunta = db.Pregunta.Find(id);
                if (pregunta == null)
                {
                    return NotFound();
                }

                db.Pregunta.Remove(pregunta);
                db.SaveChanges();

                return Ok(pregunta);
            }
            else return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreguntaExists(int id)
        {
            return db.Pregunta.Count(e => e.IdPregunta == id) > 0;
        }
    }
}