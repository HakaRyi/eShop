using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly EShopContext _context;
        public OrderDetailRepository(EShopContext context)
        {
            _context = context;
        }

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int orderId, int productId)
        {
            var availableOrderDetail = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
            if (availableOrderDetail == null) return;
            _context.OrderDetails.Remove(availableOrderDetail);
            _context.SaveChanges();
        }

        public List<OrderDetail> GetAllOrderDetail()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            var tracker = _context.OrderDetails.Attach(orderDetail);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
