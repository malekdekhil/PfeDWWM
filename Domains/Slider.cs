using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public class Slider
    {
        [Key]
        public int IdSlider { get; set; }
       
        public string Title { get; set; }
       
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Link { get; set; }
        [Required]
        public string UrlImage { get; set; }
    }
}
