using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class Authentification
    {
        [Key]
        public int id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string pseudo { get; set;  }
        [Required(AllowEmptyStrings = false)]
        public string mdp { get; set; }
        public string idDecideur { get; set; }
        public virtual Decideur decideur { get; set; }
        public Authentification()
        {

        }
    }
   

}