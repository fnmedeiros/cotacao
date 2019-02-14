using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotacao.Models;
using Cotacao.ViewModel;

namespace Cotacao.Controllers
{
    [Authorize]
    public class ItensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region MetodosPrincipais

        // GET: Itens
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Cotacao);
            return View(items.ToList());
        }

        // GET: Itens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Itens/Create
        public ActionResult Create()
        {
            ViewBag.CotacaoId = new SelectList(db.Cotacaos, "Id", "UsuarioCadastro");
            return View("Cotacoes.");
        }

        // POST: Itens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,QtdEmbalagem,ValorTotal,CotacaoId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return Redirect("/Cotacoes/Details/" + item.CotacaoId);
            }
            return RedirectToRoute(new { controller = "Cotacoes", action = "Details", id = item.Cotacao });
        }

        // GET: Itens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CotacaoId = new SelectList(db.Cotacaos, "Id", "UsuarioCadastro", item.CotacaoId);
            return View(item);
        }

        // POST: Itens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,QtdEmbalagem,ValorTotal,CotacaoId")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CotacaoId = new SelectList(db.Cotacaos, "Id", "UsuarioCadastro", item.CotacaoId);
            return View(item);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion MetodosPrincipais

        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return Json(new { resultado = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItens(int idCotacao)
        {
            var itens = db.Items.Where(x => x.CotacaoId == idCotacao);

            List<ItemViewModel> resultado = new List<ItemViewModel>();
            foreach (var item in itens)
            {
                resultado.Add(new ItemViewModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    CotacaoId = item.CotacaoId,
                    QtdEmbalagem = item.QtdEmbalagem,
                    ValorTotal = item.ValorTotal
                });
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addItem([Bind(Include = "Id,Nome,QtdEmbalagem,ValorTotal,CotacaoId")] Item item)
        {
            if (ModelState.IsValid)
            {
                Item novoItem = new Item();
                List<ItemViewModel> itemList = new List<ItemViewModel>();
                novoItem = db.Items.Add(item);
                //db.Items.Add(item);
                db.SaveChanges();
                itemList.Add(new ItemViewModel
                {
                    Id = novoItem.Id,
                    Nome = novoItem.Nome,
                    QtdEmbalagem = novoItem.QtdEmbalagem,
                    ValorTotal = novoItem.ValorTotal,
                    CotacaoId = novoItem.CotacaoId
                });
                //return Json(new { resultado = true }, JsonRequestBehavior.AllowGet);
                return Json(itemList, JsonRequestBehavior.AllowGet);
            }

            return Json(new { resultado = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
