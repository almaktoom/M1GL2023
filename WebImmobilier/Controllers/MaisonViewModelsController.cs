using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebImmobilier.Models;

namespace WebImmobilier.Controllers
{
    public class MaisonViewModelsController : Controller
    {
        private bdImmobilierContext db = new bdImmobilierContext();

        List<MaisonViewModel>  GetMaisonViewModels()
        {
            List<MaisonViewModel> liste = new List<MaisonViewModel>();
            var listeMaison = db.maisons.ToList();
            var listeBien = db.biens.ToList();

            foreach ( var item in listeMaison )
            {
                var leBien = listeBien.Where(a => a.IdBien == item.IdBien).FirstOrDefault();
                MaisonViewModel m = new MaisonViewModel();
                m.IdBien = item.IdBien;
                m.IdProprio = item.IdProprio;
                m.SuperficieBien = item.SuperficieBien;
                m.NbreSalleEau = item.NbreSalleEau;
                m.NbreChambre = item.NbreChambre;
                m.NbreCuisine = item.NbreCuisine;
                m.NbreToilette = item.NbreToilette;
                m.Localite = item.Localite;
                m.DescriptionBien = item.DescriptionBien;
                liste.Add(m);
            }

            return liste;
        }

       

        MaisonViewModel GetMaisonViewModelsById(int id)
        {
            MaisonViewModel maison = new MaisonViewModel();
            var maisonTrouvee = db.maisons.FirstOrDefault(m => m.IdBien == id);

            if (maisonTrouvee != null)
            {
                var bienAssocie = db.biens.FirstOrDefault(b => b.IdBien == maisonTrouvee.IdBien);

                if (bienAssocie != null)
                {
                    maison.IdBien = maisonTrouvee.IdBien;
                    maison.IdProprio = maisonTrouvee.IdProprio;
                    maison.SuperficieBien = maisonTrouvee.SuperficieBien;
                    maison.NbreSalleEau = maisonTrouvee.NbreSalleEau;
                    maison.NbreChambre = maisonTrouvee.NbreChambre;
                    maison.NbreCuisine = maisonTrouvee.NbreCuisine;
                    maison.NbreToilette = maisonTrouvee.NbreToilette;
                    maison.Localite = maisonTrouvee.Localite;
                    maison.DescriptionBien = maisonTrouvee.DescriptionBien;
                }
            }

            return maison;
        }

        //public DataTable GetTableMAison()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("DescriptionBien", typeof(string));
        //    table.Columns.Add("SuperficiBien", typeof(float));
        //    table.Columns.Add("Localite", typeof(string));
        //    table.Columns.Add("NbreSalleEau", typeof(int));
        //    table.Columns.Add("NbreToilette", typeof(int));
        //    table.Columns.Add("Prorietaire", typeof(string));
        //    table.Columns.Add("NbreChambre", typeof(int));

        //    var maisonViewModels = GetMaisonViewModels();
        //    foreach (var e in maisonViewModels)
        //    {
        //        table.Rows.Add(e.DescriptionBien,
        //                       e.SuperficieBien,
        //                       e.Localite,
        //                       e.NbreSalleEau,
        //                       e.NbreCuisine,
        //                       e.NbreToilette,
        //                       e.Proprietaire,
        //                       e.NbreChambre);
        //    }
        //        return table;
        //}

        //public ActionResult Imprimer()
        //{
        //    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //    try
        //    {
        //        rpt.Load(Server.MapPath("~/Report/rptListMAison.rpt"));
        //        rpt.SetDataSource(GetTableMAison());
        //        Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        Response.AppendHeader("Content-Disposition", "inline");
        //        return File(stream, "application/pdf");
        //    }finally
        //    { 
        //        rpt.Dispose();
        //        rpt.Close();
        //    }

        //}

        public DataTable GetTableMAison()
        {
            DataTable table = new DataTable();
            table.Columns.Add("DescriptionBien", typeof(string));
            table.Columns.Add("SuperficieBien", typeof(float));
            table.Columns.Add("Localite", typeof(string));
            table.Columns.Add("NbreSalleEau", typeof(int));
            table.Columns.Add("NbreToilette", typeof(int));
            table.Columns.Add("Proprietaire", typeof(string));
            table.Columns.Add("NbreChambre", typeof(int));

            var maisonViewModels = GetMaisonViewModels();

            foreach (var e in maisonViewModels)
            {
                object[] rowData = new object[7]; // Créer un nouveau tableau pour chaque ligne

                rowData[0] = e.DescriptionBien;
                rowData[1] = e.SuperficieBien;
                rowData[2] = e.Localite;
                rowData[3] = e.NbreSalleEau;
                rowData[4] = e.NbreToilette;
                rowData[6] = e.NbreChambre;

                // Récupérer les informations du propriétaire en fonction de l'ID du propriétaire
                var proprio = db.proprietaires.FirstOrDefault(p => p.IdUser == e.IdProprio);
                if (proprio != null)
                {
                    rowData[5] = $"{proprio.NomUser} {proprio.PrenomUser}";
                }
                else
                {
                    rowData[5] = "Inconnu";
                }

                table.Rows.Add(rowData);
            }



            return table;
        }

        



        public ActionResult Imprimer()
        {
            using (CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
            {
                rpt.Load(Server.MapPath("~/Report/rptListMAison.rpt"));
                rpt.SetDataSource(GetTableMAison());
                Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.AppendHeader("Content-Disposition", "inline");
                return File(stream, "application/pdf");
            }
        }

        public DataTable GetMaisonData(int id)
        {
            DataTable table = new DataTable();
            table.Columns.Add("DescriptionBien", typeof(string));
            table.Columns.Add("SuperficieBien", typeof(float));
            table.Columns.Add("Localite", typeof(string));
            table.Columns.Add("NbreSalleEau", typeof(int));
            table.Columns.Add("NbreToilette", typeof(int));
            table.Columns.Add("Proprietaire", typeof(string));
            table.Columns.Add("NbreChambre", typeof(int));

            var maisonViewModel = GetMaisonViewModelsById(id);

            if (maisonViewModel != null)
            {
                object[] rowData = new object[7]; // Créer un nouveau tableau pour une seule ligne

                rowData[0] = maisonViewModel.DescriptionBien;
                rowData[1] = maisonViewModel.SuperficieBien;
                rowData[2] = maisonViewModel.Localite;
                rowData[3] = maisonViewModel.NbreSalleEau;
                rowData[4] = maisonViewModel.NbreToilette;
                rowData[6] = maisonViewModel.NbreChambre;

                // Récupérer les informations du propriétaire en fonction de l'ID du propriétaire
                var proprio = db.proprietaires.FirstOrDefault(p => p.IdUser == maisonViewModel.IdProprio);
                if (proprio != null)
                {
                    rowData[5] = $"{proprio.NomUser} {proprio.PrenomUser}";
                }
                else
                {
                    rowData[5] = "Inconnu";
                }

                table.Rows.Add(rowData);
            }

            return table;
        }


        public ActionResult ImprimerD(int id)
        {
            using (CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument())
            {
                rpt.Load(Server.MapPath("~/Report/DetailsMAisonReport.rpt"));
                rpt.SetDataSource(GetMaisonData(id)); // Utilisez la méthode pour récupérer les données d'une seule maison
                Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.AppendHeader("Content-Disposition", "inline");
                return File(stream, "application/pdf");
            }
        }






        // GET: MaisonViewModels
        public ActionResult Index(string localite, int? nbChambres, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;

            var maisonViewModels = GetMaisonViewModels();

            // Filtrer les maisons en fonction des critères de recherche
            if (!string.IsNullOrEmpty(localite))
            {
                maisonViewModels = maisonViewModels.Where(m => m.Localite.Contains(localite)).ToList();
            }

            if (nbChambres.HasValue)
            {
                maisonViewModels = maisonViewModels.Where(m => m.NbreChambre == nbChambres).ToList();
            }

            return View(maisonViewModels.ToPagedList(pageNumber, pageSize));
        }


        // GET: MaisonViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maison maisonViewModel = db.maisons.Find(id);
            if (maisonViewModel == null)
            {
                return HttpNotFound();
            }
            return View(maisonViewModel);
        }

        // GET: MaisonViewModels/Create
        public ActionResult Create()
        {
            
            ViewBag.IdProprio = new SelectList(db.utilisateurs, "IdUser", "NomUser");
            return View();
        }

        // POST: MaisonViewModels/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBien,DescriptionBien,SuperficieBien,Localite,NbreSalleEau,NbreCuisine,NbreToilette,IdProprio,NbreChambre")] MaisonViewModel maisonViewModel)
        {
            if (ModelState.IsValid)
            {
                Maison m = new Maison();
                m.NbreCuisine = maisonViewModel.NbreCuisine;
                m.NbreChambre = maisonViewModel.NbreChambre;
                m.NbreToilette = maisonViewModel.NbreToilette;
                m.Localite = maisonViewModel.Localite;
                m.SuperficieBien = maisonViewModel.SuperficieBien;
                m.NbreSalleEau = maisonViewModel.NbreSalleEau;
                m.DescriptionBien = maisonViewModel.DescriptionBien;
                db.biens.Add(m);
                
                    // db.MaisonViewModels.Add(maisonViewModel);
                    db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProprio = new SelectList(db.proprietaires, "IdUser", "NomUser", maisonViewModel.IdProprio);
            return View(maisonViewModel);
        }

        // GET: MaisonViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maison maisonViewModel = db.maisons.Find(id);
            if (maisonViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProprio = new SelectList(db.proprietaires, "IdUser", "NomUser", maisonViewModel.IdProprio);
            return View(maisonViewModel);
        }

        // POST: MaisonViewModels/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBien,DescriptionBien,SuperficieBien,Localite,NbreSalleEau,NbreCuisine,NbreToilette,IdProprio,NbreChambre")] Maison maison)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maison).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProprio = new SelectList(db.proprietaires, "IdUser", "NomUser", maison.IdProprio);
            return View(maison);
        }

        // GET: MaisonViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maison maisonViewModel = db.maisons.Find(id);
            if (maisonViewModel == null)
            {
                return HttpNotFound();
            }
            return View(maisonViewModel);
        }

        // POST: MaisonViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maison maisonViewModel = db.maisons.Find(id);
            db.maisons.Remove(maisonViewModel);
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
