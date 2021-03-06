﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cotacao.Models;
using Microsoft.AspNet.Identity;

namespace Cotacao.Controllers
{
    [Authorize]
    public class MercadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region MetodosPrincipais
        
        // GET: Mercados
        public ActionResult Index()
        {
            return View(db.Mercadoes.ToList());
        }

        // GET: Mercados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mercado mercado = db.Mercadoes.Find(id);
            if (mercado == null)
            {
                return HttpNotFound();
            }

            //List<Cotacao> cotacoes = mercado.Cotacoes.ToList();
            ViewBag.Mercado = mercado.Nome;
            ViewBag.MercadoId = mercado.Id;
            return View(db.Cotacaos.Where(x => x.MercadoId == id).ToList());
        }

        // GET: Mercados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mercados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] Mercado mercado)
        {
            if (ModelState.IsValid)
            {
                db.Mercadoes.Add(mercado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mercado);
        }

        // GET: Mercados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mercado mercado = db.Mercadoes.Find(id);
            if (mercado == null)
            {
                return HttpNotFound();
            }
            return View(mercado);
        }

        // POST: Mercados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] Mercado mercado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mercado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mercado);
        }

        // GET: Mercados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mercado mercado = db.Mercadoes.Find(id);
            if (mercado == null)
            {
                return HttpNotFound();
            }
            return View(mercado);
        }

        // POST: Mercados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mercado mercado = db.Mercadoes.Find(id);
            db.Mercadoes.Remove(mercado);
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

        #endregion MetodosPrincipais

        public JsonResult GetMercados()
        {
            //View(db.Mercadoes.ToList());
            var resultado = db.Mercadoes.ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addMercado([Bind(Include = "Id,Nome")] Mercado mercado)
        {
            if (ModelState.IsValid)
            {
                db.Mercadoes.Add(mercado);
                db.SaveChanges();
                return Json(new { resultado = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { resultado = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
