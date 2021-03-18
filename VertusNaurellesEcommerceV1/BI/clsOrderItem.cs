using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VertusNaurellesEcommerceV1.Models;

namespace VertusNaurellesEcommerceV1.BI
{
    public interface IOrderItem
    {
        IList<OrderItem> GetAllOrderItem();
        OrderItem GetOrderItemById(int id);
        void DeleteOrderItem(int id);
        void AddOrderItem(OrderItem orderItem);
    }
    public class clsOrderItem : IOrderItem
    {
        private readonly AppDbContext db;
        public clsOrderItem(AppDbContext _db)
        {
            db = _db;
        }
        public void AddOrderItem(OrderItem orderItem)
        {
            db.TbOrderItems.Add(orderItem);
            db.SaveChanges();

        }

      

        public void DeleteOrderItem(int id)
        {
            var orderItem = db.TbOrderItems.Find(id);
            db.TbOrderItems.Remove(orderItem);
            db.SaveChanges();
        }

        public IList<OrderItem> GetAllOrderItem()
        {
            return db.TbOrderItems.ToList();
        }

        public OrderItem GetOrderItemById(int id)
        {
            return db.TbOrderItems.FirstOrDefault(c => c.IdOrderItem == id);
        }


    }
}
