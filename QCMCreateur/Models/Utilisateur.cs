using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class Utilisateur
    {

        [Required, Display(Name = "Prenom")]
        public string LastName { get; set; }
        [Required, Display(Name = "Nom")]
        public string FirstName { get; set; }
        [Key]
        [Required, Display(Name = "Identifiant")]
        public string UserName { get; set; }
        [Required, Display(Name = "Mot de passe"), DataType(DataType.Password), MinLength(8, ErrorMessage = "Le mot de passe doit contenir 8 caractères minimun")]
        public string Password { get; set; }
        [Required, Display(Name = "Confimer mot de passe"), Compare("Password", ErrorMessage = "Le mot de passe doit être identique"), DataType(DataType.Password)]
        public string ComfirmedPassword { get; set; }
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Ceci n'est pas une adresse mail")]
        public string Email { get; set; }
        public string Authorisation { get; set; }
        public static bool Connecter = false;
        public static string Nom;
        public static string log;

        public static List<string> AuthorisationListe = new List<string>
        {
            "Enseig", "Etud"
        };
    }
}