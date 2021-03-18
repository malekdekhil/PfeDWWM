using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface IOrder
    {
        IList<Order> GetAllOrder();
        Order GetOrderById(int id);
        void DeleteOrder(int id);
        void AddOrder(Order order);
    }
    public class clsOrder : IOrder
    {
        private readonly AppDbContext db;
        public clsOrder(AppDbContext _db)
        {
            db = _db;
        }
        public void AddOrder(Order order)
        {
            db.TbOrders.Add(order);
            db.SaveChanges();

        }

     

        public void DeleteOrder(int id)
        {
            var bOrder = db.TbOrders.Find(id);
            db.TbOrders.Remove(bOrder);
            db.SaveChanges();
        }

        public IList<Order> GetAllOrder()
        {
            return db.TbOrders.ToList();
        }

    

        public Order GetOrderById(int id)
        {
            return db.TbOrders.FirstOrDefault(c => c.IdOrder == id);
        }

      
    }
}
