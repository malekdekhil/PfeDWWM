using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;

namespace VertusNaurellesEcommerceV1.Models
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
     
       
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
            
        }
        public DbSet<Category> TbCategories { get; set; }
        public DbSet<Product> TbProducts { get; set; }
        public DbSet<Opinion> TbOpinions { get; set; }
        public DbSet<OrderItem> TbOrderItems { get; set; }
        public DbSet<Order> TbOrders { get; set; }
        public DbSet<Slider> TbSliders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
      
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}


