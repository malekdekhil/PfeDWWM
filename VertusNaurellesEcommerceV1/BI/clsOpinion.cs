using Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface IOpinion
    {
        IList<Opinion> GetAllOpinons();
        void Add(Opinion opinion);
        Opinion GetOpinionById(int id);


    }
    public class clsOpinion : IOpinion
    {
        private readonly AppDbContext db;

        public clsOpinion(AppDbContext _db)
        {
            db = _db;
        }


        public void Add(Opinion opinion)
        {
    
            db.TbOpinions.Add(opinion);
            db.SaveChanges();
        }

        public IList<Opinion> GetAllOpinons()
        {
            return db.TbOpinions.ToList();
        }

        public Opinion GetOpinionById(int id)
        {
           return db.TbOpinions.FirstOrDefault(o => o.IdOpinion == id);
        }
    }
}
