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
        public IQueryable<Pregunta> GetPregunta()
        {
            return db.Pregunta;
        }

        // GET: api/Preguntas/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult GetPregunta(int id)
        {
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return Ok(pregunta);
        }

        // PUT: api/Preguntas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPregunta(int id, Pregunta pregunta)
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

        // POST: api/Preguntas
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult PostPregunta(Pregunta pregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pregunta.Add(pregunta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pregunta.IdPregunta }, pregunta);
        }

        // DELETE: api/Preguntas/5
        [ResponseType(typeof(Pregunta))]
        public IHttpActionResult DeletePregunta(int id)
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