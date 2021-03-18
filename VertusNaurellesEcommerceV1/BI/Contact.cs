using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.BI
{
    public class Contact
    {
        public string  MyEmail { get; set; }
        [Required(ErrorMessage ="Veuillez saisire votre nom")]
        public string  FirstName { get; set; }
        [Required(ErrorMessage = "Veuillez saisire votre prénom")]
        public string  LastName { get; set; }
        [Required(ErrorMessage = "Veuillez saisire l'objet")]
        public string  sujet { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Votre e-mail n' pas valide")]
        public string  Email { get; set; }
        [Required(ErrorMessage = "Veuillez saisire votre message")]
        [MinLength(50,ErrorMessage = "Le message doit contenir au moins 50 caractère")]
        public string  Message { get; set; }
    }
}
