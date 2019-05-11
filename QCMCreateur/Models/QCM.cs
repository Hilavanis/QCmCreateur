using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class QCM
    {

        [Key]
        [Required]
        public string NameId { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Reponse { get; set; }
        [Required]
        public string BonneReponse { get; set; }
        [Required]
        public List<string> Difficulter = new List<string>()
        { "6eme", "5eme", "4eme", "3eme", "seconde", "premiere", "Terminal" };
        [Required]
        public List<string> Matiere = new List<string>()
        { "Français", "Mathematique", "Anglais", "Chimie", "Physique", "Biologie", "SVT", "Histoire", "Geographie" };
        
    }
}