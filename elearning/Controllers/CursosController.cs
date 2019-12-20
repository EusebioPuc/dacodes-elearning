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
    public class CursosController : ApiController
    {
        private elearningEntities db = new elearningEntities();

        // GET: api/Cursos
        public IQueryable<Curso> GetCurso()
        {
            return db.Curso;
        }

        // GET: api/Cursos/5
        [ResponseType(typeof(Curso))]
        public IHttpActionResult GetCurso(int id)
        {
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        // PUT: api/Cursos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurso(int id, Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.IdCurso)
            {
                return BadRequest();
            }

            db.Entry(curso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
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

        // POST: api/Cursos
        [ResponseType(typeof(Curso))]
        public IHttpActionResult PostCurso(Curso curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Curso.Add(curso);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = curso.IdCurso }, curso);
        }

        // DELETE: api/Cursos/5
        [ResponseType(typeof(Curso))]
        public IHttpActionResult DeleteCurso(int id)
        {
            Curso curso = db.Curso.Find(id);
            if (curso == null)
            {
                return NotFound();
            }

            db.Curso.Remove(curso);
            db.SaveChanges();

            return Ok(curso);
        }

        /// <summary>
        /// Obtiene una lista de todos los cursos, indicando a cuáles puede acceder el estudiante
        /// Si puede acceder es porque esta inscrito
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/CursosAlumno/5
        [HttpGet]
        [ResponseType(typeof(List<CursoAlumnoVM>))]
        public IHttpActionResult CursosAlumno(int id)
        {
            List<CursoAlumnoVM> cursos = new List<CursoAlumnoVM>();
            Usuario alumno= db.Usuario.Where(x=> x.IdUsuario==id && x.IdRol==3 && x.Estatus=="ACTIVO").FirstOrDefault();
            if (alumno!=null)
            {
                List<AlumnoCurso> lsAlumnoCursos = db.AlumnoCurso.Where(x=> x.IdAlumno==alumno.IdUsuario).ToList();
                List<Curso> lsCursos = db.Curso.Where(x => x.Estatus=="ACTIVO").ToList();

                if (lsCursos.Count()>0)
                {
                    foreach (Curso item in lsCursos)
                    {
                        CursoAlumnoVM icurso = new CursoAlumnoVM();
                        icurso.IdCurso = item.IdCurso;
                        icurso.Curso1 = item.Curso1;
                        icurso.Descripcion = item.Descripcion;
                        List<AlumnoCurso> lsRAlumnoCursos= lsAlumnoCursos.Where(x=>x.IdCurso==item.IdCurso).ToList();
                        if (lsRAlumnoCursos.Count()>0)
                            icurso.PuedeAcceder = true;
                        else
                            icurso.PuedeAcceder = false;
                                               
                        cursos.Add(icurso);
                    }
                }else
                    return NotFound();

                CursoAlumnoVM cursoVM = new CursoAlumnoVM();
                if (cursos == null)
                {
                    return NotFound();
                }
            }else
                return NotFound();

            return Ok(cursos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursoExists(int id)
        {
            return db.Curso.Count(e => e.IdCurso == id) > 0;
        }
    }
}