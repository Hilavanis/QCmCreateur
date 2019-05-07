using QCMCreateur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QCMCreateur.Controllers
{
    public class UtilisateurController : Controller
    {
        QcmCreateurContext qcmcreateur = new QcmCreateurContext();
        
        // GET: Account
        public ActionResult SeConnecter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SeConnecter(Utilisateur u)
        {
            var count = qcmcreateur.EUtilisateurs.Where(c => c.UserName == u.UserName && c.Password == u.Password).Count();
            if(count == 0)
            {
                ViewBag.Message = "Utilisateur non valide";
                return View();
            }
            else
            {
                var utilisateurliste = qcmcreateur.EUtilisateurs.ToList();

                foreach(Utilisateur util in utilisateurliste)
                {
                    if(util.UserName == u.UserName)
                    {
                        Utilisateur.Nom = util.LastName;
                        Utilisateur.log = util.UserName;
                        if(util.Authorisation == "Admin")
                        {
                            Administrateur.AdminOk = true;
                        }
                        else if(util.Authorisation == "Enseig")
                        {
                            Enseignant.EnseigOk = true;
                        }
                    }
                }
                Utilisateur.Connecter = true;
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Deconnection()
        {
            Administrateur.AdminOk = false;
            Enseignant.EnseigOk = false;
            Utilisateur.Nom = null;
            Utilisateur.log = null;
            Utilisateur.Connecter = false;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MonCompte()
        {
            var utilisateurliste = qcmcreateur.EUtilisateurs.ToList();
            foreach (Utilisateur util in utilisateurliste)
            {
                if(Utilisateur.log == util.UserName)
                {
                    return View(util);
                }
            }
            return View();
        }

        public ActionResult ModifierPassword(string id)
        {
            Utilisateur util = qcmcreateur.EUtilisateurs.Find(id);
            return View(util);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierPassword([Bind(Include = "UserName, Password, ComfirmedPassword")]Utilisateur util)
        {
            var query = from data in qcmcreateur.EUtilisateurs
                        where data.UserName == util.UserName
                        select data;
            foreach (Utilisateur details in query)
            {
                details.Password = util.Password;
                details.ComfirmedPassword = util.ComfirmedPassword;
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("MonCompte");
        }

        public ActionResult ModifierEmail(string id)
        {
            Utilisateur util = qcmcreateur.EUtilisateurs.Find(id);
            return View(util);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifierEmail([Bind(Include = "UserName, Email")]Utilisateur util)
        {
            var query = from data in qcmcreateur.EUtilisateurs
                        where data.UserName == util.UserName
                        select data;
            foreach (Utilisateur details in query)
            {
                details.Email = util.Email;
            }
            qcmcreateur.SaveChanges();

            return RedirectToAction("MonCompte");
        }
    }
}