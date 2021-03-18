using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;

namespace Domains
{

    public class Product
    {

        [Key]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Nom de l'article")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Nom de réferencement")]
        public string SeoName { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Prix d'achat")]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 99999.99, ErrorMessage = "Le champ {0} doit être compris entre 1 et 99999.99")]
        [RegularExpression("[+-]?([0-9]*[.])?[0-9]+", ErrorMessage = "Non valide ex 123456.12")]

        public decimal PurchasePrice { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Prix de vente")]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 99999.99, ErrorMessage = "Le champ {0} doit être compris entre 1 et 99999.99")]
        [RegularExpression("[+-]?([0-9]*[.])?[0-9]+", ErrorMessage = "Non valide ex 123456.12")]

        public decimal SalesPrice { get; set; }
        [Display(Name = "Promotion")]

        public bool InPromo { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Courte description")]

        public string ShortDescription { get; set; }
        [Display(Name = "Longue description")]

        public string LongDescription { get; set; }

        [Display(Name = "lien de l'image")]

        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Quantité")]

        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Date de création")]

        public DateTime CreationDate { get; set; }
        [ForeignKey("Category")]
        [Display(Name = "Catégorie")]

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Opinion> Opinions { get; set; }

     
    }
}
