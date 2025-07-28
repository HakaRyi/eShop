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
    
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();


                foreach (var detail in orderDetails)
                {
                    detail.OrderId = order.OrderId;
                    _context.OrderDetails.Add(detail);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order.OrderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
