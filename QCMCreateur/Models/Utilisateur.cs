using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class Utilisateur
    {
        private string _lastName;
        private string _firstName;
        private string _userName;
        private string _password;
        private string _comfirmedPassword;
        private string _email;
        private string _authorisation;

        [Required, Display(Name = "Prenom")]
        public string LastName { get => _lastName; set => _lastName = value; }
        [Required, Display(Name = "Nom")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        [Key]
        [Required, Display(Name = "Identifiant")]
        public string UserName { get => _userName; set => _userName = value; }
        [Required, Display(Name = "Mot de passe"), DataType(DataType.Password), MinLength(8, ErrorMessage = "Le mot de passe doit contenir 8 caractères minimun")]
        public string Password { get => _password; set => _password = value; }
        [Required, Display(Name = "Confimer mot de passe"), Compare("Password", ErrorMessage = "Le mot de passe doit être identique"), DataType(DataType.Password)]
        public string ComfirmedPassword { get => _comfirmedPassword; set => _comfirmedPassword = value; }
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Ceci n'est pas une adresse mail")]
        public string Email { get => _email; set => _email = value; }
        public string Authorisation { get => _authorisation; set => _authorisation = value; }
        public static bool Connecter = false;
        public static string Nom;
        public static string log;

        public static List<string> AuthorisationListe = new List<string>
        {
            "Enseig", "Etud"
        };
    }
}