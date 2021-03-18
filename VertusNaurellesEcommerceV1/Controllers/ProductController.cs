using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;
using VertusNaurellesEcommerceV1.Models;
using VertusNaurellesEcommerceV1.Models.ViewsModels;

namespace VertusNaurellesEcommerceV1.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProduct productService;
        private readonly ICategory categorytService;
        private readonly IOpinion opinionService;
        private readonly UserManager<AppUser> CustomerManager;

        public ProductController(IProduct _productService, ICategory _categorytService, IOpinion _opinionService, UserManager<AppUser> _CustomerManager)
        {
            productService = _productService;
            categorytService = _categorytService;
            opinionService = _opinionService;
            CustomerManager = _CustomerManager;

        }
        public IActionResult Search(string term)
        {
            var result = productService.Search(term);
            return View(result);
        }
        public IActionResult Index()
        {
            var Products = new ProductViewModel();
            Products.ListProducts = productService.GetAllProducts().Where(i=>i.Quantity>=1);
      
            
            return View(Products);
        }
        public IActionResult Details(int id)
        {
            var currentCustomer = this.User;
            var CustomerName = CustomerManager.GetUserName(currentCustomer);
            
            var vm = new PageDetailsProductViewModel ();
            vm.ListProduct = productService.GetAllProducts();
            vm.product = productService.GetProductById(id);
            vm.ListOpinion = opinionService.GetAllOpinons();
            //UserName = CustomerManager.GetUserName(currentCustomer);

            //var Product = productService.GetProductById(id);
            if (vm.product == null ) return RedirectToAction("Index", "Product"); 
            return View(vm);
        }
        public IActionResult ProductInCategory(int? id)
        {
           
            var category = productService.GetAllProductByCategory(Convert.ToInt32(id));
            if (id != null)
            {
                return View(category);
            }

            return RedirectToAction("Index", "Product");



        }
        [Authorize]
        public async Task<IActionResult>SaveComment(Opinion opinion,int id )
        {
          
            AppUser user = await CustomerManager.FindByEmailAsync(User.Identity.Name);

            var currentCustomer = this.User;
            var CustomerName = CustomerManager.GetUserName(currentCustomer);


            opinion.UserName = user.FirstName.ToUpper()+" "+user.LastName.ToLower();
            ViewBag.note = opinionService.GetAllOpinons().ToList();
            if (ModelState.IsValid)
            {
                if(opinion.ProductId > 0 && opinion.Note > 0 && currentCustomer != null)
                {
                    opinionService.Add(opinion);

                }

                return RedirectToAction("Details", "Product", new { id = opinion.ProductId });
            }
            else
            {
     
                return RedirectToAction("Details", "Product", new { id = opinion.ProductId });

            }
            
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            try
            {
                //Ajout de la session dans lobjet oShoppinCart 
                CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
                // je verifie si lobjet est vide ou pas si vide je fait une nouvelle instance
                if (oShoppingCart == null)
                    oShoppingCart = new CartViewModel();
                Product product = productService.GetProductById(id);
                CartItemViewModel ItemCart = oShoppingCart.ListProducts.Where(a => a.ProductId == id).FirstOrDefault();
                // si larticle existe deja dans le panier j'incremente la qte 
                if (ItemCart != null)
                {
                    //verif de la quantitée en dbb avan d'incrementer
                    if (product.Quantity > ItemCart.ProductQuantity)
                    {
                        ItemCart.ProductQuantity++;

                    }
                    ItemCart.Total = ItemCart.ProductPrice * ItemCart.ProductQuantity;
                }
                else
                {
                    oShoppingCart.ListProducts.Add(new CartItemViewModel()
                    {
                        ProductId = product.IdProduct,
                        ProductName = product.Name,
                        ProductUrlImage = product.ImageUrl,
                        ProductQuantity = 1,
                        ProductPrice = product.SalesPrice,
                        Total = product.SalesPrice,
                    });
                }

                oShoppingCart.Total = oShoppingCart.ListProducts.Sum(a => a.Total);
                oShoppingCart.TotalAllItems = oShoppingCart.ListProducts.Sum(t => t.Total);

                HttpContext.Session.SetObjectAsJson("Cart", oShoppingCart);

                return Redirect("/Home/Index");
            }
            catch
            {
                return Redirect("/Home/Index");
            }

        }

     







    }
}
