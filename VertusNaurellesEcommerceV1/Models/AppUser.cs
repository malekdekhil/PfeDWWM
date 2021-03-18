using Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VertusNaurellesEcommerceV1.Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
       
        public string Address { get; set; }
      
        public string City { get; set; }

        public int CodeZip { get; set; }
    
        public string Country { get; set; }
        [ForeignKey("Opinion")]
        public List<Opinion> Opinion { get; set; }
     
        [ForeignKey("Order")]
        public List<Order> Order { get; set; }
        [ForeignKey("OrderItem")]
        public List<OrderItem> OrderItem { get; set; }
    }
}
