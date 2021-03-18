using Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models.ViewsModels
{
    public class AdminProductCategoryViewModel
    {
        
        public int IdProduct { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string SeoName { get; set; }
        [Required]

        public decimal PurchasePrice { get; set; }
        [Required]

        public decimal SalesPrice { get; set; }

        public bool InPromo { get; set; }
        [Required]

        public string ShortDescription { get; set; }
        [Required]

        public string LongDescription { get; set; }
   
        [Required]

        public int Quantity { get; set; }
        [Required]

        public DateTime CreationDate { get; set; }
        [Required]

        public int CategoryId { get; set; }

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
