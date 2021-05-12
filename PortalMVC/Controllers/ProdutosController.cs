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
using Microsoft.AspNet.Identity;

namespace PortalMVC.Controllers
{
    public class ProdutosController : Controller
    {
        private MercadoNaturalEntities db = new MercadoNaturalEntities();

        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            var produtos = db.Produtos.Include(p => p.Empregado);
            return View(await produtos.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            Empregado empregado = db.Empregados.FirstOrDefault(c => c.Id == produto.IdEmpregado);

            return View(produto);
        }

        //Apenas autorizados (logados) poderão criar um novo produto.
        [Authorize]
        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.IdEmpregado = new SelectList(db.Empregados, "Id", "Nome");
            return View();
        }

        //Apenas autorizados (logados) poderão criar um novo produto.
        [Authorize]
        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Preco,IdEmpregado,DataCriacao")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                //Identificar o empregado que está logado para relacionar com o produto e adicionar a data atual na Data de inserção do produto.
                string userEmail = User.Identity.GetUserName();
                Empregado empregado = db.Empregados.FirstOrDefault(c => c.Email == userEmail);
                produto.IdEmpregado = empregado.Id;
                produto.DataCriacao = DateTime.Now;
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpregado = new SelectList(db.Empregados, "Id", "Nome", produto.IdEmpregado);
            return View(produto);
        }

        //Apenas autorizados (logados) poderão editar um produto.
        [Authorize]
        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            string userEmail = User.Identity.GetUserName();
            Empregado empregado = db.Empregados.FirstOrDefault(c => c.Email == userEmail);
            //Se o empregado logado não é o gestor deste produto, não poderá editá-lo
            if (produto.IdEmpregado != empregado.Id)
            {
                return RedirectToAction("SemPermissao", "Home", new { area = "" });
            }
            ViewBag.IdEmpregado = new SelectList(db.Empregados, "Id", "Nome", produto.IdEmpregado);
            return View(produto);
        }

        //Apenas autorizados (logados) poderão editar um produto.
        [Authorize]
        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Preco,IdEmpregado,DataCriacao")] Produto produto)
        {
            //string userEmail = User.Identity.GetUserName();
            //Empregado empregado = db.Empregados.FirstOrDefault(c => c.Email == userEmail);
           // Produto bdproduto = db.Produtos.FirstOrDefault(c => c.IdEmpregado == userEmail);

            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpregado = new SelectList(db.Empregados, "Id", "Nome", produto.IdEmpregado);
            return View(produto);
        }

        //Apenas autorizados (logados) poderão deletar um produto.
        [Authorize]
        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            string userEmail = User.Identity.GetUserName();
            Empregado empregado = db.Empregados.FirstOrDefault(c => c.Email == userEmail);
            //Se o empregado logado não é o gestor deste produto, não poderá editá-lo
            if (produto.IdEmpregado != empregado.Id)
            {
                return RedirectToAction("SemPermissao", "Home", new { area = "" });
            }
            return View(produto);
        }

        //Apenas autorizados (logados) poderão deletar um produto.
        [Authorize]
        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            string userEmail = User.Identity.GetUserName();
            Empregado empregado = db.Empregados.FirstOrDefault(c => c.Email == userEmail);
            //Se o empregado logado não é o gestor deste produto, não poderá editá-lo
            if (produto.IdEmpregado != empregado.Id)
            {
                return RedirectToAction("SemPermissao", "Home", new { area = "" });
            }
            db.Produtos.Remove(produto);
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
