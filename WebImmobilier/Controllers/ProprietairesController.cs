using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebImmobilier.Models;
using PagedList.Mvc;
using PagedList;

namespace WebImmobilier.Controllers
{
    public class ProprietairesController : Controller
    {
        private bdImmobilierContext db = new bdImmobilierContext();


        // GET: Proprietaires
        public ActionResult Index(string nom, string prenom, string login, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 1;

            var proprietairesList = db.proprietaires.ToList();

            // Filtrer les propriétaires en fonction des critères de recherche (insensible à la casse)
            if (!string.IsNullOrEmpty(nom))
            {
                nom = nom.ToLower(); // Convertir en minuscules
                proprietairesList = proprietairesList.Where(p => p.NomUser.ToLower().Contains(nom)).ToList();
            }

            if (!string.IsNullOrEmpty(prenom))
            {
                prenom = prenom.ToLower(); // Convertir en minuscules
                proprietairesList = proprietairesList.Where(p => p.PrenomUser.ToLower().Contains(prenom)).ToList();
            }

            if (!string.IsNullOrEmpty(login))
            {
                login = login.ToLower(); // Convertir en minuscules
                proprietairesList = proprietairesList.Where(p => p.Login.ToLower().Contains(login)).ToList();
            }

            var pagedList = proprietairesList.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }



        // GET: Proprietaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietaire proprietaire = (Proprietaire)db.utilisateurs.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            return View(proprietaire);
        }

        // GET: Proprietaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proprietaires/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUser,NomUser,PrenomUser,Login,Password")] Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(proprietaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proprietaire);
        }

        // GET: Proprietaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietaire proprietaire = (Proprietaire)db.utilisateurs.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            return View(proprietaire);
        }

        // POST: Proprietaires/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUser,NomUser,PrenomUser,Login,Password")] Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proprietaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proprietaire);
        }

        // GET: Proprietaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietaire proprietaire = (Proprietaire)db.utilisateurs.Find(id);
            if (proprietaire == null)
            {
                return HttpNotFound();
            }
            return View(proprietaire);
        }

        // POST: Proprietaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proprietaire proprietaire = (Proprietaire)db.utilisateurs.Find(id);
            db.utilisateurs.Remove(proprietaire);
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
