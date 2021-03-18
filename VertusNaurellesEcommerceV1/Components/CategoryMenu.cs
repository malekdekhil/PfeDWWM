using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.BI;

    
namespace VertusNaurellesEcommerceV1.Components
{
    public class CategoryMenu:ViewComponent
    {
        private readonly ICategory categorytService;

        public CategoryMenu(ICategory _categorytService)
        {
            categorytService = _categorytService;

        }

        public IViewComponentResult Invoke()
        {
            var ItemsCategories = categorytService.GetAllCategories();
            return View(ItemsCategories);

        }

    }
}
