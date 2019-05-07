using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class EnseignantController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        public ActionResult AfficherListeEnseignant()
        {
            var enseignant = qcmcreateur.EEnseignant.ToList();
            return View(enseignant);
        }

        public ActionResult Details(string id)
        {
            Enseignant enseignant = qcmcreateur.EEnseignant.Find(id);
            return View(enseignant);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ModifierAuthorisation(string id)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var autho in Utilisateur.AuthorisationListe)
            {
                list.Add(new SelectListItem() { Value = autho, Text = autho });
            }

            ViewBag.authorisation = list;
            Enseignant enseignant = qcmcreateur.EEnseignant.Find(id);
            return View(enseignant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierAuthorisation([Bind(Include = "Authorisation, UserName, LastName, FirstName, Password, ComfirmedPassword Email ")]Enseignant enseignant)
        {
            var query = from data in qcmcreateur.EUtilisateurs
                        where data.UserName == enseignant.UserName
                        select data;
            foreach (Utilisateur details in query)
            {
                if (enseignant.Authorisation == "Etud")
                {
                    details.Authorisation = enseignant.Authorisation;
                    Etudiant etudiant = new Etudiant() { LastName = details.LastName, FirstName = details.FirstName, UserName = details.UserName, Password = details.Password, ComfirmedPassword = details.ComfirmedPassword, Email = details.Email, Authorisation = details.Authorisation };
                    qcmcreateur.EEtudiant.Add(etudiant);
                    Enseignant enseig = qcmcreateur.EEnseignant.Find(details.UserName);
                    qcmcreateur.EEnseignant.Remove(enseig);
                    break;
                }
                break;
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeEnseignant");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SupprimerEnseignant(string id)
        {
            Enseignant enseignant = qcmcreateur.EEnseignant.Find(id);

            return View(enseignant);
        }

        [HttpPost, ActionName("SupprimerEnseignant")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimerSupprimer(string id)
        {
            Enseignant enseignant = qcmcreateur.EEnseignant.Find(id);
            qcmcreateur.EEnseignant.Remove(enseignant);
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeEnseignant");
        }
    }
}