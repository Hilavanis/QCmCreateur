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
    }
}