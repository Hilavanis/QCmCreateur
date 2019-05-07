using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class EtudiantController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        public ActionResult AfficherListeEtudiant()
        {
            var etudiant = qcmcreateur.EEtudiant.ToList();
            return View(etudiant);
        }

        public ActionResult Inscription()
        {
            Etudiant etudiant = new Etudiant();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription(Etudiant etudiant)
        {
            var listeutilisateur = qcmcreateur.EUtilisateurs.ToList();
            foreach (Utilisateur utilisateur in listeutilisateur)
            {
                if (etudiant.UserName.ToLower() == utilisateur.UserName.ToLower())
                {
                    ViewBag.message = "identifiant déjà utilisé";
                    return View();
                }
            }

            qcmcreateur.EEtudiant.Add(etudiant);
            qcmcreateur.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(string id)
        {
            Etudiant etudiant = qcmcreateur.EEtudiant.Find(id);
            return View(etudiant);
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
            Etudiant etudiant = qcmcreateur.EEtudiant.Find(id);
            return View(etudiant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierAuthorisation([Bind(Include = "Authorisation, UserName, LastName, FirstName, Password, ComfirmedPassword Email ")]Etudiant etudiant)
        {
            var query = from data in qcmcreateur.EUtilisateurs
                        where data.UserName == etudiant.UserName
                        select data;
            foreach (Utilisateur details in query)
            {

                if (etudiant.Authorisation == "Enseig")
                {
                    details.Authorisation = etudiant.Authorisation;
                    Enseignant ensei = new Enseignant() { LastName = details.LastName, FirstName = details.FirstName, UserName = details.UserName, Password = details.Password, ComfirmedPassword = details.ComfirmedPassword, Email = details.Email, Authorisation = details.Authorisation };
                    qcmcreateur.EEnseignant.Add(ensei);
                    Etudiant etude = qcmcreateur.EEtudiant.Find(details.UserName);
                    qcmcreateur.EEtudiant.Remove(etude);
                    break;
                }
                break;
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeEtudiant");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SupprimerEtudiant(string id)
        {
            Etudiant etudiant = qcmcreateur.EEtudiant.Find(id);

            return View(etudiant);
        }

        [HttpPost, ActionName("SupprimerEtudiant")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimerSupprimer(string id)
        {
            Etudiant etudiant = qcmcreateur.EEtudiant.Find(id);
            qcmcreateur.EEtudiant.Remove(etudiant);
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeEtudiant");
        }
    }
}