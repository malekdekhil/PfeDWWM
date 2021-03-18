using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        public string PaymentTransactionId { get; set; }
        public System.DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal OrderTotal { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("OrderItem")]

        public OrderItem OrderItems { get; set; }
    }
}