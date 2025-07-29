using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class OrderRepository
    {
        private readonly EShopContext _context;
        public OrderRepository(EShopContext context)
        {
            _context = context;
        }
        public async Task<Order> GetPendingOrderByMemberIdAsync(int memberId)
        {
            var order = await _context.Orders
                .Where(o => o.MemberId == memberId && o.Status == OrderStatus.Pending)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                order = new Order
                {
                    MemberId = memberId,
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            return order;
        }
        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<OrderDetail> FindOrderDetailAsync(int orderDetailId)
        {
            return await _context.OrderDetails.FindAsync(orderDetailId);
        }
        public async Task<Order> FindOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }
        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = OrderStatus.Success;
                if (status == OrderStatus.Success)
                {
                    order.ShippedDate = DateTime.Now.AddDays(3); 
                }
                order.OrderDate = DateTime.Now; 
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<OrderDetail>> GetOrderDetailsByMemberIdAsync(int memberId)
        {
            return await _context.OrderDetails
                .Where(od => od.Order.MemberId == memberId && od.Order.Status == OrderStatus.Success)
                .Include(od => od.Product)
                .Include(od => od.Order)
                .ToListAsync();
        }
    }
}
