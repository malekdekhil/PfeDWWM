using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
   public class Opinion
    {
        [Key]
        public int IdOpinion { get; set; }
        public string Commentaire { get; set; }

        public double Note { get; set; }
        public string UserName { get; set; }
        [ForeignKey("Product")]

        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
