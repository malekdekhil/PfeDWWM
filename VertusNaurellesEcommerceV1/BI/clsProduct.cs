using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VertusNaurellesEcommerceV1.Models;
using VertusNaurellesEcommerceV1.Models.ViewsModels;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface IProduct
    {
        IList<Product> GetAllProducts();
         Task<List<Product>> GetAllProductsAsync();   
        Product GetProductById(int id);
        IList<Product> GetAllProductByCategory(int id);
        void AddProduct(Product product);
        void DeleteProduct(int id);
        Product UpdateProduct(int id, Product newProduct);
        IList<Product> Search(string term);

    }


    public class clsProduct : IProduct
    {
        private readonly AppDbContext db;
        public clsProduct(AppDbContext _db)
        {
            db = _db;
        }

     

        public IList<Product> GetAllProductByCategory(int id)
        {
            return db.TbProducts.Where(a => a.CategoryId == id).ToList();
        }

        public IList<Product> GetAllProducts()
        {
            return db.TbProducts.Include(c=>c.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            return db.TbProducts.FirstOrDefault(p => p.IdProduct == id);
        }
        public void DeleteProduct(int id)
        {
            var productDelete = db.TbProducts.Find(id);
            db.TbProducts.Remove(productDelete);
            db.SaveChanges();
        }
        public Product UpdateProduct(int id, Product newProduct)
        {
            var product = db.TbProducts.Find(id);
            product.Name = newProduct.Name;
            product.SeoName = newProduct.SeoName;
            product.PurchasePrice = newProduct.PurchasePrice;
            product.SalesPrice = newProduct.SalesPrice;
            product.ShortDescription = newProduct.ShortDescription;
            product.LongDescription = newProduct.LongDescription;
            product.InPromo = newProduct.InPromo;
            product.ImageUrl = newProduct.ImageUrl;
            product.CategoryId = newProduct.CategoryId;
            product.Quantity = newProduct.Quantity;
            product.CreationDate = newProduct.CreationDate;
            db.SaveChanges();
            return product;
        }

        public void AddProduct(Product product)
        {
            db.TbProducts.Add(product);
            db.SaveChanges(); 
        }
        
        public IList<Product> Search(string term)
        {
            var result = db.TbProducts.Include(c=>c.Category)
            .Where(
                n=>n.Name.Contains(term)
                || n.LongDescription.Contains(term)|| n.ShortDescription.Contains(term) 
                || n.Category.CategoryName.Contains(term)|| n.Category.SeoNameCategory.Contains(term) 
                || n.Category.Description.Contains(term) || n.SeoName.Contains(term)
                    ).ToList();


            return result;
        }

      

        async Task<List<Product>> IProduct.GetAllProductsAsync()
        {
            List<Product> list = await Task.Run(() => db.TbProducts.ToListAsync());
            
            return list;
        }
    }
}
