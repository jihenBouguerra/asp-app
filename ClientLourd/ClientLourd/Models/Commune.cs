using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class Commune
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int id { get; set; }
        public string nomArab { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string nomfr { get; set; }
        public string selectedGouv { get; set; }
        public virtual Gouvernorat gouvernorat { get; set; }
        public virtual ICollection<DecideurCommune> decideurs { get; set; }
       
       public virtual ICollection<Graph> graphs { get; set; }
   
        public Commune()
        {
            decideurs = new List<DecideurCommune>();
            graphs = new List<Graph>();
        } 

    }
}