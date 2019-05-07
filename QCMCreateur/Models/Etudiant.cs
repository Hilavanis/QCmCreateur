using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class Etudiant : Utilisateur
    {
        public Etudiant()
        {
            Authorisation = "Etud";
        }
    }
}