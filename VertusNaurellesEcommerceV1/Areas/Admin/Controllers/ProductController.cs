using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;
using VertusNaurellesEcommerceV1.Models.ViewsModels;

namespace VertusNaurellesEcommerceV1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        readonly IProduct productService;
        readonly ICategory categoryService;

        public ProductController(IProduct _productService, ICategory _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;

        }
        // GET: ProductController
        public ActionResult Index()
        {
           
            return View(productService.GetAllProducts());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            
            return View(productService.GetProductById(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.categories = categoryService.GetAllCategories().ToList();


            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IFormFile file)
        {
            
                try
                {
                    string FileName = string.Empty;
                    if (file != null)
                    {
                    // nom du fichier que je vais uploder
                    FileName = Guid.NewGuid()+file.FileName;
                    //Path du fichier pour enregistrer mes images
                    string ProductImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/ProductsImages", FileName);
                    using (var stream = System.IO.File.Create(ProductImage))
                    {
                       
                        file.CopyTo(stream);

                    }
                    //Path complet
                    //string FullPath = Path.Combine(ProductImage,FileName);
                    //    //l'action a effectuer
                    //    file.CopyTo(new FileStream(FullPath, FileMode.Create));
                    }
                       product.ImageUrl = FileName;

                    productService.AddProduct(product);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.categories = categoryService.GetAllCategories().ToList();

            if (id != null)
            {
                return View(productService.GetProductById(Convert.ToInt32(id)));
            }
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id , Product product, IFormFile file)
        {
            try
            {
                string FileName = string.Empty;
                if (file != null)
                {
                 // create 
                    FileName = Guid.NewGuid() + file.FileName;
                     string OldPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Images\ProductsImages", product.ImageUrl);
                     string NewPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Images\ProductsImages", FileName);


                    //copy new file 
                    using (var stream = System.IO.File.Create(NewPath))
                    {
                        //file.CopyTo(new FileStream(NewPath, FileMode.Create));
                        //file.CopyTo(new FileStream(NewPath, FileMode.CreateNew));
                        file.CopyTo(stream);

                    }
                    //delete oldFile
                    if (product.ImageUrl != null)
                    {
                        System.IO.File.Delete(OldPath);

                    }
                  
                    product.ImageUrl = FileName;
                    productService.UpdateProduct(id, product);
        
                }
                else
                {
                    product.ImageUrl = product.ImageUrl;
                    productService.UpdateProduct(id, product);
                }
             

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                e.Message.ToString();
                return View();
            }
        }

        //GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {

            return View(productService.GetProductById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Product product)
        {
            try
            {
                string PathRoot = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\images\ProductsImages",product.ImageUrl);
                //string OldPath = Path.Combine(PathRoot, product.ImageUrl);
                System.IO.File.Delete(PathRoot);
                productService.DeleteProduct(id);





                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return View();
            }
        }
    }
}
