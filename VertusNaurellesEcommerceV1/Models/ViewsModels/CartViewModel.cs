using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models.ViewsModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            ListProducts = new List<CartItemViewModel>();
        }
        public List<CartItemViewModel> ListProducts { get; set; }
        public Decimal Total { get; set; }
        public Decimal TotalAllItems { get; set; }

    }
}
