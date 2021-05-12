using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Net.Http;

namespace PortalMVC.Controllers
{
    public class EmpregadosController : Controller
    {
        private MercadoNaturalEntities db = new MercadoNaturalEntities();

        // GET: Empregados
        public async Task<ActionResult> Index()
        {
            return View(await db.Empregados.ToListAsync());
        }

        // GET: Empregados/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = await db.Empregados.FindAsync(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // GET: Empregados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empregados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Email,DataNascimento,DataInicio")] Empregado empregado)
        {
            if (ModelState.IsValid)
            {
                db.Empregados.Add(empregado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(empregado);
        }

        // GET: Empregados/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = await db.Empregados.FindAsync(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // POST: Empregados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Email,DataNascimento,DataInicio")] Empregado empregado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empregado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(empregado);
        }

        // GET: Empregados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = await db.Empregados.FindAsync(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // POST: Empregados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empregado empregado = await db.Empregados.FindAsync(id);
            db.Empregados.Remove(empregado);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
