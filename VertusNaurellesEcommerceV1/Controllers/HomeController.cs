using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;
using VertusNaurellesEcommerceV1.Models;
using VertusNaurellesEcommerceV1.Models.ViewsModels;

namespace VertusNaurellesEcommerceV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct productService;
        private readonly ICategory categorytService;
        private readonly ISlider sliderService;
        private readonly IOpinion opinionService;
        public HomeController(IProduct _productService, ICategory _categorytService, ISlider _sliderService, IOpinion _opinionService)
        {
            productService = _productService;
            categorytService = _categorytService;
            sliderService = _sliderService;
            opinionService = _opinionService;
        }
        public IActionResult Index()
        {
   
            var Products = new ProductViewModel();
            Products.ListProducts = productService.GetAllProducts();
            
            Products.ListSliders = sliderService.GetAllSlider();
            ViewBag.FirstSlider = sliderService.GetAllSlider().FirstOrDefault().UrlImage;
            ViewBag.FirstSliderId = sliderService.GetAllSlider().FirstOrDefault().IdSlider;
         


            return View(Products);
        }
        [HttpGet]
        public IActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {

            if ( ModelState.IsValid)
            {
               string myEmail =  contact.MyEmail = "malekdekhil68@gmail.com";
                MailMessage message = new MailMessage(myEmail, contact.Email);
                message.Subject = contact.sujet;
                message.Body = contact.Message;
                SmtpClient client = new SmtpClient(contact.ToString());
                client.UseDefaultCredentials = true;
                client.Send(message);

                
           

                // envoi du mail
            }
            else
            {
                RedirectToAction("Contact");
            }

            return View(contact);
        }


    }
}
