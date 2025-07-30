using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EShopContext _context;
        public OrderRepository(EShopContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            var availableOrder = _context.Orders.FirstOrDefault(a => a.OrderId == order.OrderId);
            if (availableOrder == null) return;
            _context.Orders.Remove(availableOrder);
            _context.SaveChanges();
        }

        public List<Order> GetAllOrder()
        {
           return _context.Orders.ToList();
        }

        public Order GetOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(a => a.OrderId == id);
            return order;
        }

        public List<SalesReportItem> GetSalesReport(DateTime start, DateTime end)
        {
            var result = (from od in _context.OrderDetails
                          join o in _context.Orders on od.OrderId equals o.OrderId
                          join p in _context.Products on od.ProductId equals p.ProductId
                          where o.OrderDate >= start && o.OrderDate <= end
                          group new { od, p } by new { od.ProductId, p.ProductName } into g
                          orderby g.Sum(x => x.od.Quantity * x.od.UnitPrice) descending
                          select new SalesReportItem
                          {
                              ProductId = g.Key.ProductId,
                              ProductName = g.Key.ProductName,
                              TotalQuantity = g.Sum(x => x.od.Quantity),
                              TotalRevenue = g.Sum(x => x.od.Quantity * x.od.UnitPrice)
                          }).ToList();

            return result;
        }

        public void UpdateOrder(Order order)
        {
            var tracker = _context.Orders.Attach(order);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
