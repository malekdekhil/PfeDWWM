using System;
using Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models.ViewsModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> ListProducts { get; set; }
        public Category category { get; set; }
        public IEnumerable<Slider> ListSliders { get; set; }
    }
}
