using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class MatiereetQCMController : Controller
    {
        //Matiere
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        public ActionResult AfficherListeMatiere()
        {
            var matiere = qcmcreateur.EMatiere.ToList();
            return View(matiere);
        }

        //QCM
        public ActionResult AfficherListeQCM(string id)
        {
            Matiere matiere = qcmcreateur.EMatiere.Find(id);
            var listeqcm = qcmcreateur.EQCM.Where(c => c.MatiereId == matiere.MatiereId).ToList();
            return View(listeqcm);
        }

        public ActionResult CreerQCM()
        {
            QCM qcm = new QCM();
            List<SelectListItem> list = new List<SelectListItem>();

            var listeMatiere = qcmcreateur.EMatiere.ToList();
            foreach (Matiere m in listeMatiere)
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
                        if (qcm.Name == qcms.Name)
                        {
                            i++;
                            qcm.Name = "QCM" + i;
                        }
                        else
                            break;
                    }
                    qcmcreateur.EQCM.Add(qcm);
                }
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeMatiere");
        }

        public ActionResult RepondreQCM(string id)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Matiere matiere = qcmcreateur.EMatiere.Find(id);
            var listeqcm = qcmcreateur.EQCM.Where(c => c.MatiereId == matiere.MatiereId).ToList();
            QCM selection = listeqcm[rand.Next(0, listeqcm.Count)];
            return View(selection);
        }

        [HttpPost]
        public ActionResult RepondreQCM([Bind(Include = "Name, MatiereId, Question, ReponseUne, ReponseDeux, ReponseTrois, BonneReponse, CocherUn, CocherDeux") ] QCM qcm)
        {
            if(qcm.CocherUn == true && qcm.CocherDeux == true)
            {
                ViewBag.reponseko = "Une seule réponse possible";
            }
            else if (qcm.CocherDeux == true)
            {
                ViewBag.reponseok = "Correct";
            }
            else if (qcm.CocherUn == true)
            {
                ViewBag.reponseko = "Faux";
            }
            
            return View (qcm);
        }
    }
}