using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
   public class OrderItem
    {
        public OrderItem()
        {

        }
        [Key]
        public int IdOrderItem { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Product product { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public Decimal Price { get; set; }

    }
}
