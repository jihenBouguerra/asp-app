using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClientLourd.Models
{
    public class compte
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "CIN is required")]
        [RegularExpression("(?!00000000)(^[0-9]{8}$)", ErrorMessage = "Invalid cin")]
        [Index("scoreIndex", IsUnique = true)]
        public int cin { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string nom { get; set; }
        [Required(AllowEmptyStrings = false)]

        public string prenom { get; set; }
        public string variable { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Email { get; set; }
        
        
        public bool ministre { get; set; }
        public virtual ICollection<Authentification> authentifications { get; set; }

        public compte()
        {
            authentifications = new List<Authentification>();


        }

    }
}