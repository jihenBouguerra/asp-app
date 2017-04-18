﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class Graph
    {

        [System.ComponentModel.DataAnnotations.Key]
        public int Id{ get; set; }
        [Required(AllowEmptyStrings = false)]
        public string valeur { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string titre { get; set; }
        public virtual Commune commune { get; set; }
        public virtual Gouvernorat gouvernorat { get; set; }
        /* public virtual ICollection<Authentification> authentifications { get; set; }

         public Graph()
         {
             authentifications = new List<Authentification>();
         }*/
    }
}