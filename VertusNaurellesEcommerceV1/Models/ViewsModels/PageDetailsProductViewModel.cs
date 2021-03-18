using Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models.ViewsModels
{
    public class PageDetailsProductViewModel
    {
        public PageDetailsProductViewModel()
        {

        }
        public IEnumerable<Product> ListProduct { get; set; }
        public Product product { get; set; }
        public IEnumerable<Opinion> ListOpinion { get; set; }

       

    }
}
