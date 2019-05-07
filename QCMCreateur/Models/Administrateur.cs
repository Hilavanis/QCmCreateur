using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class Administrateur : Utilisateur
    {
        public static bool AdminOk;
        public Administrateur()
        {
            Authorisation = "Admin";
        }
    }
}