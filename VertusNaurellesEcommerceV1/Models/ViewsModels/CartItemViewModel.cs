using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models.ViewsModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrlImage { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Total { get; set; }

      
    }

}
