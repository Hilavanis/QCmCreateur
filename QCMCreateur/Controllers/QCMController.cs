using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class QCMController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        public ActionResult CreerQCM()
        {
            QCM qcm = new QCM();
            List<SelectListItem> list = new List<SelectListItem>();

            var listeMatiere = qcmcreateur.EMatiere.ToList();
            foreach(Matiere m in listeMatiere)
            {
                qcm.ListeMatiere.Add(m.MatiereId);
            }

            foreach (var matiere in qcm.ListeMatiere)
            {
                list.Add(new SelectListItem() { Value = matiere, Text = matiere });
            }

            ViewBag.matiere = list;
            return View();
        }

        [HttpPost]
        public ActionResult CreerQCM(QCM qcm)
        {
            int i = 1;
            qcm.Name = "QCM" + i;


            var query = qcmcreateur.EMatiere.ToList();

            foreach (Matiere matiere in query)
            {
                var listematiere = matiere.ListeQCM.ToList();

                if (matiere.MatiereId == qcm.MatiereSelectionner)
                {
                    foreach (QCM listeqcm in listematiere)
                    {
                        while (qcm.Name == listeqcm.Name)
                        {
                            i++;
                            qcm.Name = "QCM" + i;
                        }
                        break;
                    }
                    matiere.ListeQCM.Add(qcm);
                    int verif = matiere.ListeQCM.Count();
                }
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeMatiere", "Matiere");
        }
    }
}