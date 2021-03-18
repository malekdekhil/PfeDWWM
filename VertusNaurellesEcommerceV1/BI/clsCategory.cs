using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface ICategory
    {
        IList<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void DeleteCategory(int id);
        void AddCategory(Category category);
        Category UpdateCategory(int id, Category newCategory);
    }
    public class clsCategory : ICategory
    {
        private readonly AppDbContext db;
        public clsCategory(AppDbContext _db)
        {
            db = _db;
        }
        public void AddCategory(Category category)
        {
            db.TbCategories.Add(category);
            db.SaveChanges();

        }
        public void DeleteCategory(int id)
        {
            var CategorieDel = db.TbCategories.Find(id);
            db.TbCategories.Remove(CategorieDel);
            db.SaveChanges();
        }

        public IList<Category> GetAllCategories()
        {
            return db.TbCategories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return db.TbCategories.FirstOrDefault(c=>c.IdCategory == id);
        }

        public Category UpdateCategory(int id, Category newCategory)
        {
            var category = db.TbCategories.Find(id);
            category.CategoryName = newCategory.CategoryName;
            category.Description = newCategory.Description;
            category.SeoNameCategory = newCategory.SeoNameCategory;
            db.SaveChanges();
            return category;
        }
    }
}
