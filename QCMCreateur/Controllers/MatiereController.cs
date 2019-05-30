using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class MatiereController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        public ActionResult AfficherListeMatiere()
        {
            var matiere = qcmcreateur.EMatiere.ToList();
            return View(matiere);
        }

        public ActionResult AfficherListeQCM(string id)
        {
            Matiere matiere = qcmcreateur.EMatiere.Find(id);
            var listeqcm = qcmcreateur.EQCM.Where(c => c.MatiereId == matiere.MatiereId).ToList();
            return View(listeqcm);
        }

        public ActionResult CreerMatiere()
        {
            Matiere matiere = new Matiere();
            return View();
        }

        [HttpPost]
        public ActionResult CreerMatiere(Matiere matiere)
        {
            qcmcreateur.EMatiere.Add(matiere);
            qcmcreateur.SaveChanges();
            return RedirectToAction("AfficherListeMatiere");
        }
    }
}