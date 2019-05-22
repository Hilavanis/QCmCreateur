using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QCMCreateur.Controllers
{
    public class AdministrateurController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AfficherListeAdmin()
        {
            var admin = qcmcreateur.EAdministrateur.ToList();
            return View(admin);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreerAdmin()
        {
            Administrateur admin = new Administrateur();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreerAdmin(Administrateur admin)
        {
            int i = 1;
            var listeutilisateur = qcmcreateur.EUtilisateurs.ToList();
            admin.UserName = admin.LastName.Substring(0, 3).ToLower() + i + admin.FirstName.Substring(0,3).ToLower();
            foreach(Utilisateur utilisateur in listeutilisateur)
            {
                while(admin.UserName == utilisateur.UserName)
                {
                    i++;
                    admin.UserName = admin.LastName.Substring(0, 3).ToLower() + i + admin.FirstName.Substring(0, 3).ToLower();
                }
                break;
            }
            qcmcreateur.EAdministrateur.Add(admin);
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeAdmin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SupprimerAdmin(string id)
        {
            Administrateur admin = qcmcreateur.EAdministrateur.Find(id);

            return View(admin);
        }
                
        [HttpPost, ActionName("SupprimerAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfimerSupprimer(string id)
        {
            Administrateur admin = qcmcreateur.EAdministrateur.Find(id);
            qcmcreateur.EAdministrateur.Remove(admin);
            qcmcreateur.SaveChanges();

            return RedirectToAction("AfficherListeAdmin");
        }        
    }
}