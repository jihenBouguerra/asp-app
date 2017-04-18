using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClientLourd.Models
{
    public class Decideur
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "CIN is required")]
        [RegularExpression("(?!00000000)(^[0-9]{8}$)", ErrorMessage = "Invalid cin")]
        [Index("scoreIndex", IsUnique = true)   ]
        public  int cin { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string nom { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string prenom { get; set; }
       
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
     
        public string Email { get; set; }

        public virtual Commune commune { get; set; }
        public virtual Gouvernorat gouvernorat { get; set; }
         public virtual ICollection<Authentification> authentifications { get; set; }

         public Decideur()
         {
             authentifications = new List<Authentification>();
            

         } 

    }
    
}