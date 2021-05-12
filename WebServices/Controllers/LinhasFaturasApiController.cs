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
    public class LinhasFaturasApiController : ApiController
    {
        private MercadoNaturalEntities db = new MercadoNaturalEntities();

        // GET: api/LinhasFaturas
        public IQueryable<LinhasFatura> GetLinhasFatura()
        {
            return db.LinhasFatura;
        }

        // GET: api/LinhasFaturas/5
        [ResponseType(typeof(LinhasFatura))]
        public async Task<IHttpActionResult> GetLinhasFatura(int id)
        {
            LinhasFatura linhasFatura = await db.LinhasFatura.FindAsync(id);
            if (linhasFatura == null)
            {
                return NotFound();
            }

            return Ok(linhasFatura);
        }

        // PUT: api/LinhasFaturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLinhasFatura(int id, LinhasFatura linhasFatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != linhasFatura.Id)
            {
                return BadRequest();
            }

            db.Entry(linhasFatura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinhasFaturaExists(id))
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

        // POST: api/LinhasFaturas
        [ResponseType(typeof(LinhasFatura))]
        public async Task<IHttpActionResult> PostLinhasFatura(LinhasFatura linhasFatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LinhasFatura.Add(linhasFatura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = linhasFatura.Id }, linhasFatura);
        }

        // DELETE: api/LinhasFaturas/5
        [ResponseType(typeof(LinhasFatura))]
        public async Task<IHttpActionResult> DeleteLinhasFatura(int id)
        {
            LinhasFatura linhasFatura = await db.LinhasFatura.FindAsync(id);
            if (linhasFatura == null)
            {
                return NotFound();
            }

            db.LinhasFatura.Remove(linhasFatura);
            await db.SaveChangesAsync();

            return Ok(linhasFatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinhasFaturaExists(int id)
        {
            return db.LinhasFatura.Count(e => e.Id == id) > 0;
        }
    }
}