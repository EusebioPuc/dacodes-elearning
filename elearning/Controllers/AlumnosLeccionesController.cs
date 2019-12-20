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
    public class AlumnosLeccionesController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/AlumnosLecciones
        public IQueryable<AlumnoLeccion> GetAlumnoLeccion()
        {
            return db.AlumnoLeccion;
        }

        // GET: api/AlumnosLecciones/5
        [ResponseType(typeof(AlumnoLeccion))]
        public IHttpActionResult GetAlumnoLeccion(int id)
        {
            AlumnoLeccion alumnoLeccion = db.AlumnoLeccion.Find(id);
            if (alumnoLeccion == null)
            {
                return NotFound();
            }

            return Ok(alumnoLeccion);
        }

        // PUT: api/AlumnosLecciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlumnoLeccion(int id, AlumnoLeccion alumnoLeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumnoLeccion.IdAlumnoLeccion)
            {
                return BadRequest();
            }

            db.Entry(alumnoLeccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoLeccionExists(id))
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

        // POST: api/AlumnosLecciones
        [ResponseType(typeof(AlumnoLeccion))]
        public IHttpActionResult PostAlumnoLeccion(AlumnoLeccion alumnoLeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlumnoLeccion.Add(alumnoLeccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumnoLeccion.IdAlumnoLeccion }, alumnoLeccion);
        }

        // DELETE: api/AlumnosLecciones/5
        [ResponseType(typeof(AlumnoLeccion))]
        public IHttpActionResult DeleteAlumnoLeccion(int id)
        {
            AlumnoLeccion alumnoLeccion = db.AlumnoLeccion.Find(id);
            if (alumnoLeccion == null)
            {
                return NotFound();
            }

            db.AlumnoLeccion.Remove(alumnoLeccion);
            db.SaveChanges();

            return Ok(alumnoLeccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumnoLeccionExists(int id)
        {
            return db.AlumnoLeccion.Count(e => e.IdAlumnoLeccion == id) > 0;
        }
    }
}