using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotacao.Models;

namespace Cotacao.Controllers
{
    [Authorize]
    public class CotacoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cotacoes
        public ActionResult Index()
        {
            var cotacaos = db.Cotacaos.Include(c => c.Mercado);
            return View(cotacaos.ToList());
        }

        // GET: Cotacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotacao cotacao = db.Cotacaos.Find(id);
            if (cotacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mercado = cotacao.Mercado.Nome;
            ViewBag.UsuarioCadastro = cotacao.UsuarioCadastro;
            ViewBag.Data = cotacao.Date;
            ViewBag.CotacaoId = cotacao.Id;
            return View(db.Items.Where(x => x.CotacaoId == id).ToList());
            //return View(db.Cotacaos.Where(x => x.MercadoId == id).ToList());
        }

        // GET: Cotacoes/Create
        public ActionResult Create(int mercadoId)
        {
            //ViewBag.MercadoId = new SelectList(db.Mercadoes, "Id", "Nome");
            //ViewBag.MercadoId = new SelectList(db.Mercadoes.Where(x => x.Id == mercadoId).ToList(), "Id", "Nome");
            Mercado mercado = db.Mercadoes.Find(mercadoId);
            ViewBag.MercadoId = mercado.Id;
            ViewBag.Mercado = mercado.Nome;
            return View();
        }

        // POST: Cotacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,UsuarioCadastro,MercadoId")] Cotacao cotacao)
        {
            if (ModelState.IsValid)
            {
                db.Cotacaos.Add(cotacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MercadoId = new SelectList(db.Mercadoes, "Id", "Nome", cotacao.MercadoId);
            return View(cotacao);
        }

        // GET: Cotacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotacao cotacao = db.Cotacaos.Find(id);
            if (cotacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.MercadoId = new SelectList(db.Mercadoes, "Id", "Nome", cotacao.MercadoId);
            return View(cotacao);
        }

        // POST: Cotacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,UsuarioCadastro,MercadoId")] Cotacao cotacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cotacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MercadoId = new SelectList(db.Mercadoes, "Id", "Nome", cotacao.MercadoId);
            return View(cotacao);
        }

        // GET: Cotacoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cotacao cotacao = db.Cotacaos.Find(id);
            if (cotacao == null)
            {
                return HttpNotFound();
            }
            return View(cotacao);
        }

        // POST: Cotacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cotacao cotacao = db.Cotacaos.Find(id);
            db.Cotacaos.Remove(cotacao);
            db.SaveChanges();
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
