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

    public class SliderController : Controller
    {
        readonly ISlider sliderService;

        public SliderController(ISlider _sliderService)
        {
            sliderService = _sliderService;

        }
        // GET: SliderController
        public ActionResult Index()
        {
           
            return View(sliderService.GetAllSlider());
        }

        // GET: SliderController/Details/5
        public ActionResult Details(int id)
        {
            
            return View(sliderService.GetSliderById(id));
        }

        // GET: SliderController/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider Slider, IFormFile file)
        {
            
                try
                {
                    string FileName = string.Empty;
                    if (file != null)
                    {
                    // nom du fichier que je vais uploder
                    FileName = Guid.NewGuid()+file.FileName;
                    //Path du fichier pour enregistrer mes images
                    string SliderImage = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/SliderImages", FileName);
                    using (var stream = System.IO.File.Create(SliderImage))
                    {
                       
                        file.CopyTo(stream);

                    }
                   
                    }
                       Slider.UrlImage = FileName;

                    sliderService.AddSlider(Slider);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
        }

        // GET: SliderController/Edit/5
        public ActionResult Edit(int? id)
        {
            

            if (id != null)
            {
                return View(sliderService.GetSliderById(Convert.ToInt32(id)));
            }
            return View();
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id , Slider Slider, IFormFile file)
        {
            try
            {
                string FileName = string.Empty;
                if (file != null)
                {
                 // create 
                    FileName = Guid.NewGuid() + file.FileName;
                     string OldPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Images\SliderImages", Slider.UrlImage);
                     string NewPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Images\SliderImages", FileName);


                    //copy new file 
                    using (var stream = System.IO.File.Create(NewPath))
                    {
                       
                        file.CopyTo(stream);

                    }
                    //delete oldFile
                    if (Slider.UrlImage != null)
                    {
                        System.IO.File.Delete(OldPath);

                    }
                  
                    Slider.UrlImage = FileName;
                    sliderService.UpdateSlider(id, Slider);
        
                }
                else
                {
                    Slider.UrlImage = Slider.UrlImage;
                    sliderService.UpdateSlider(id, Slider);
                }
             

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                e.Message.ToString();
                return View();
            }
        }

        //GET: SliderController/Delete/5
        public ActionResult Delete(int id)
        {

            return View(sliderService.GetSliderById(id));
        }

        // POST: SliderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Slider Slider)
        {
            try
            {
                string PathRoot = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\images\SliderImages",Slider.UrlImage);
                System.IO.File.Delete(PathRoot);
                sliderService.DeleteSlider(id);


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
