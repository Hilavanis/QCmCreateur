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

            var listematiere = qcmcreateur.EMatiere.ToList();

            foreach (Matiere matieres in listematiere)
            {
                if (matieres.MatiereId == qcm.MatiereSelectionner)
                {
                    qcm.MatiereId = matieres.MatiereId;

                    var listeqcm = qcmcreateur.EQCM.Where(c => c.MatiereId == matieres.MatiereId).ToList();

                    foreach (QCM qcms in listeqcm)
                    {
                        while (qcm.Name == qcms.Name)
                        {
                            i++;
                            qcm.Name = "QCM" + i;
                        }
                        break;
                    }
                    qcmcreateur.EQCM.Add(qcm);
                }
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeMatiere", "Matiere");
        }
    }
}