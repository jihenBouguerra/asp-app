using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class Gouvernorat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nomArab { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string nomfr { get; set; }
        public virtual ICollection<Commune> communes { get; set; }
        public virtual ICollection<Decideur> decideurs { get; set; }
  
          public virtual ICollection<Graph> graphs { get; set; }
         
        
        public Gouvernorat()
        {
            communes = new List<Commune>();
            decideurs = new List<Decideur>();
           graphs = new List<Graph>();
        }
    }
}