using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class QCM
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public string Name { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Matiere")]
        public string MatiereId { get; set; }
        public Matiere Matiere { get; set; }
        
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

        public bool CocherUn { get; set; }
        public bool CocherDeux { get; set; }

        public List<string> ListeMatiere = new List<string>();
        public string MatiereSelectionner { get; set; }
    }
}