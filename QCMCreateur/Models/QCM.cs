using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class QCM
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string ReponseUne { get; set; }
        [Required]
        public string ReponseDeux { get; set; }
        [Required]
        public string ReponseTrois { get; set; }
        [Required]
        public string BonneReponse { get; set; }
        [Key]
        [Required]
        public string Name { get; set; }

        public List<string> ListeMatiere = new List<string>();
        public string MatiereSelectionner { get; set; }
    }
}