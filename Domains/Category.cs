using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [Required]
        [Display(Name = "Nom")]

        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Nom de réferencement")]
        public string SeoNameCategory { get; set; }


        public List<Product> products { get; set; }


    }
}
