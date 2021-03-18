using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface ISlider
    {
        //jutilise ienumerable pour interroger les données en memoire
        IList<Slider> GetAllSlider();
        Slider GetSliderById(int id);
        void AddSlider(Slider slider);
        void DeleteSlider(int id);
        Slider UpdateSlider(int id, Slider newSlider);

    }


    public class clsSlider : ISlider
    {
        private readonly AppDbContext db;
        public clsSlider(AppDbContext _db)
        {
            db = _db;
        }

     

      

        public IList<Slider> GetAllSlider()
        {
            return db.TbSliders.ToList();
        }

        public Slider GetSliderById(int id)
        {
            return db.TbSliders.FirstOrDefault(p => p.IdSlider == id);
        }
        public void DeleteSlider(int id)
        {
            var silderDelete = db.TbSliders.Find(id);
            db.TbSliders.Remove(silderDelete);
            db.SaveChanges();
        }
        public Slider UpdateSlider(int id, Slider newSlider)
        {
            var slider = db.TbSliders.Find(id);
            slider.Title = newSlider.Title;
            slider.Description1 = newSlider.Description1;
            slider.Description2 = newSlider.Description2;
            slider.UrlImage = newSlider.UrlImage;
            slider.Link = newSlider.Link;
            db.SaveChanges();
            return slider;
        }

        public void AddSlider(Slider slider)
        {
            db.TbSliders.Add(slider);
            db.SaveChanges(); 
        }
        
       




    }
}
