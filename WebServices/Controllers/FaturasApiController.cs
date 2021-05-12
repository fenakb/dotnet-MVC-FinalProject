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
    public class FaturasApiController : ApiController
    {
        private MercadoNaturalEntities db = new MercadoNaturalEntities();

        // GET: api/Faturas
        public IQueryable<Fatura> GetFaturas()
        {
            return db.Faturas;
        }

        // GET: api/Faturas/5
        [ResponseType(typeof(Fatura))]
        public async Task<IHttpActionResult> GetFatura(int id)
        {
            Fatura fatura = await db.Faturas.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }

            return Ok(fatura);
        }

        // PUT: api/Faturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFatura(int id, Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fatura.Id)
            {
                return BadRequest();
            }

            db.Entry(fatura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaturaExists(id))
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

        // POST: api/Faturas
        [ResponseType(typeof(Fatura))]
        public async Task<IHttpActionResult> PostFatura(Fatura fatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faturas.Add(fatura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fatura.Id }, fatura);
        }

        // DELETE: api/Faturas/5
        [ResponseType(typeof(Fatura))]
        public async Task<IHttpActionResult> DeleteFatura(int id)
        {
            Fatura fatura = await db.Faturas.FindAsync(id);
            if (fatura == null)
            {
                return NotFound();
            }

            db.Faturas.Remove(fatura);
            await db.SaveChangesAsync();

            return Ok(fatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaturaExists(int id)
        {
            return db.Faturas.Count(e => e.Id == id) > 0;
        }
    }
}