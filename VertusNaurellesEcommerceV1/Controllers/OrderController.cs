using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;
using VertusNaurellesEcommerceV1.Models;
using VertusNaurellesEcommerceV1.Models.ViewsModels;

namespace VertusNaurellesEcommerceV1.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IProduct productService;
        private readonly IOrder orderService;
        private readonly IOrderItem orderItemService;
        private readonly UserManager<AppUser> CustomerManager;

        public OrderController(IProduct _productService, UserManager<AppUser> _CustomerManager, IOrderItem _orderItemService, IOrder _orderService)
        {
            productService = _productService;
            CustomerManager = _CustomerManager;
            orderItemService = _orderItemService;
            orderService = _orderService;
        }
        public IActionResult Index()
        {

            return View();
        }
    

        public IActionResult Cancel()
        {
            return View();
        }
        public IActionResult History()
        {
            //.Where(i => i.CustomerId == (currentCustomer).ToString()).ToList();
            var currentCustomer = this.User;
            var customer = CustomerManager.GetUserId(currentCustomer);
            ViewBag.getName = productService.GetAllProducts();
            return View(orderItemService.GetAllOrderItem().Where(p=>p.CustomerId == (customer).ToString()));
        }

       
        public void  CheckDisponibility(CartViewModel oShoppingCart, List<Domains.Product> allProductsEmpty)
        {

            foreach (var i in allProductsEmpty)
            {
                if (oShoppingCart != null)
                {
                    if (allProductsEmpty != null)
                    {


                        oShoppingCart.ListProducts.Remove(oShoppingCart.ListProducts.Where(a => a.ProductId == i.IdProduct).FirstOrDefault());
                        oShoppingCart.TotalAllItems = oShoppingCart.ListProducts.Sum(a => a.Total);
                    }
                }
            }
        }
        [Authorize]
        public IActionResult Cart()
        {
            CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            var allProductsEmpty = productService.GetAllProducts().Where(a => a.Quantity <= 0).ToList();
            var products = productService.GetAllProducts().ToList();

           CheckDisponibility(oShoppingCart, allProductsEmpty);

            HttpContext.Session.SetObjectAsJson("Cart", oShoppingCart);

            return View(oShoppingCart);
        }

     

        public IActionResult RemoveItem(int id)
        {
            CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            oShoppingCart.ListProducts.Remove(oShoppingCart.ListProducts.Where(a => a.ProductId == id).FirstOrDefault());
            oShoppingCart.TotalAllItems = oShoppingCart.ListProducts.Sum(a => a.Total);
            HttpContext.Session.SetObjectAsJson("Cart", oShoppingCart);
            return RedirectToAction("Cart");
        }

        public bool  SaveOrderItem()
        {
            //insert orderItem en bdd
            CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");

            if (oShoppingCart != null)
            {
                var currentCustomer = this.User;
                var CustomerId = CustomerManager.GetUserId(currentCustomer);

                var orderItem = orderItemService;
                foreach (var i in oShoppingCart.ListProducts)
                {
                    
                    orderItem.AddOrderItem(new Domains.OrderItem { ProductId = i.ProductId, Quantity = i.ProductQuantity, CustomerId = CustomerId, Price = i.ProductPrice ,ProductName = i.ProductName});

                }
                return true;

            }
            else
            {
                return false;
              
            }

        }


        [HttpGet]
        public ActionResult Stripe( )
        {

            CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");

            ViewBag.TotalAllItems = oShoppingCart.TotalAllItems;

            return View(oShoppingCart);

        }
       
        [HttpPost]
        public async Task<IActionResult> Stripe(string stripeEmail, string stripeToken,Domains.Order order)
        {
            var currentCustomer = this.User;
            var CustomerName = CustomerManager.GetUserName(currentCustomer);
            var CustomerID = CustomerManager.GetUserId(currentCustomer);
            CartViewModel oShoppingCart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            var allProductsEmpty = productService.GetAllProducts().Where(p=>p.Quantity <=0).ToList();

            foreach (var i in allProductsEmpty)
            {
                var IdEmptyInBdd = i.IdProduct;
                var shopping =  oShoppingCart.ListProducts.ToList();
                foreach (var sh in shopping)
                {
                    if (i.IdProduct == sh.ProductId)
                    {

                        return Redirect("/Order/cart/");

                    }
                }
                

            }
            var customers = new CustomerService();
       
            var charges = new ChargeService();
            var customer = await customers.CreateAsync(new CustomerCreateOptions() {
     
                Email=stripeEmail,
                Source = stripeToken
            });

            var charge = await charges.CreateAsync(new ChargeCreateOptions { 
                
                Amount = Decimal.ToInt64(oShoppingCart.TotalAllItems * 100),
                Customer = customer.Id ,
                Currency ="EUR",
                Description= "Paiement de "+ CustomerName
                
            });
            if (charge.Status.ToLower() == "succeeded")
            {
                SaveOrderItem();
                //création de lordre
                var oOrder = orderService;

                oOrder.AddOrder(new Domains.Order
                {
                    CustomerId = CustomerID,
                    OrderDate = DateTime.Now,
                    OrderTotal = oShoppingCart.TotalAllItems,
                    PaymentTransactionId = charge.Id


                });
                //je deduit du stock
                var prods = productService.GetAllProducts();
                foreach (var p in prods)
                {
                    foreach (var s in oShoppingCart.ListProducts)
                    {
                        if(p.IdProduct == s.ProductId)
                        {
                            p.Quantity = p.Quantity - s.ProductQuantity;

                            productService.UpdateProduct(p.IdProduct,p);
                        }
                       
                    }
                    
                }
               
                //detruire la sessions 

                     oShoppingCart.ListProducts.Clear();
                     oShoppingCart.TotalAllItems = 0;
                     HttpContext.Session.SetObjectAsJson("Cart", oShoppingCart);
                // redirection vers une page remerciment 

                return  RedirectToAction("History", "Order");
                //une fois sur la page de remerci redirection espace membre dans historique

            }
            else
            {
                // faire une page paiement refuser
                RedirectToAction("Cancel", "Order");

              


            }

            return View();

        }



    }
}