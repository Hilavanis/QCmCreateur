using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QCMCreateur.Models
{
    public class Matiere
    {
        [Key]
        [Required]
        public string MatiereId { get; set; }
        public List<QCM> EQCM { get; set; }

        public Matiere()
        {
            EQCM = new List<QCM>();
        }

        public List<QCM> AfficherQcm()
        {
            var qcm = EQCM.ToList();
            return qcm;
        }

        public void AjouterQcm(QCM qcm)
        {
            EQCM.Add(qcm);
        }
    }
}