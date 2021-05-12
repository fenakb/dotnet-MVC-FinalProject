using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;

namespace WebServices.Controllers
{
    public class EmpregadosApiController : ApiController
    {
        private MercadoNaturalEntities db = new MercadoNaturalEntities();

        // GET: api/Empregados
        public IQueryable<Empregado> GetEmpregados()
        {
            return db.Empregados;
        }

        // GET: api/Empregados/5
        [ResponseType(typeof(Empregado))]
        public async Task<IHttpActionResult> GetEmpregado(int id)
        {
            Empregado empregado = await db.Empregados.FindAsync(id);
            if (empregado == null)
            {
                return NotFound();
            }

            return Ok(empregado);
        }

        // PUT: api/Empregados/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmpregado(int id, Empregado empregado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empregado.Id)
            {
                return BadRequest();
            }

            db.Entry(empregado).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpregadoExists(id))
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

        // POST: api/Empregados
        [ResponseType(typeof(Empregado))]
        public async Task<IHttpActionResult> PostEmpregado(Empregado empregado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empregados.Add(empregado);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = empregado.Id }, empregado);
        }

        // DELETE: api/Empregados/5
        [ResponseType(typeof(Empregado))]
        public async Task<IHttpActionResult> DeleteEmpregado(int id)
        {
            Empregado empregado = await db.Empregados.FindAsync(id);
            if (empregado == null)
            {
                return NotFound();
            }

            db.Empregados.Remove(empregado);
            await db.SaveChangesAsync();

            return Ok(empregado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpregadoExists(int id)
        {
            return db.Empregados.Count(e => e.Id == id) > 0;
        }
    }
}