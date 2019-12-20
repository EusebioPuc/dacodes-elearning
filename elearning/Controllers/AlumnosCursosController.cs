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
    public class AlumnosCursosController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/AlumnosCursos
        public IQueryable<AlumnoCurso> GetAlumnoCurso()
        {
            return db.AlumnoCurso;
        }

        // GET: api/AlumnosCursos/5
        [ResponseType(typeof(AlumnoCurso))]
        public IHttpActionResult GetAlumnoCurso(int id)
        {
            AlumnoCurso alumnoCurso = db.AlumnoCurso.Find(id);
            if (alumnoCurso == null)
            {
                return NotFound();
            }

            return Ok(alumnoCurso);
        }

        // PUT: api/AlumnosCursos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlumnoCurso(int id, AlumnoCurso alumnoCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumnoCurso.IdAlumnoCurso)
            {
                return BadRequest();
            }

            db.Entry(alumnoCurso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoCursoExists(id))
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

        // POST: api/AlumnosCursos
        [ResponseType(typeof(AlumnoCurso))]
        public IHttpActionResult PostAlumnoCurso(AlumnoCurso alumnoCurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AlumnoCurso.Add(alumnoCurso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumnoCurso.IdAlumnoCurso }, alumnoCurso);
        }

        // DELETE: api/AlumnosCursos/5
        [ResponseType(typeof(AlumnoCurso))]
        public IHttpActionResult DeleteAlumnoCurso(int id)
        {
            AlumnoCurso alumnoCurso = db.AlumnoCurso.Find(id);
            if (alumnoCurso == null)
            {
                return NotFound();
            }

            db.AlumnoCurso.Remove(alumnoCurso);
            db.SaveChanges();

            return Ok(alumnoCurso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumnoCursoExists(int id)
        {
            return db.AlumnoCurso.Count(e => e.IdAlumnoCurso == id) > 0;
        }
    }
}