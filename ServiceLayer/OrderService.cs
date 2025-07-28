using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;
using RepositoryLayer;
using ServiceLayer.DTO;

namespace ServiceLayer
{
    public class OrderService
    {
        private readonly OrderRepository orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<int> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = new Order
            {
                MemberId = orderDto.MemberId,
                OrderDate = orderDto.OrderDate,
                Freight = 0 
            };

            var orderDetails = orderDto.Items.Select(item => new OrderDetail
            {
                ProductId = item.ProductId,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                Discount = 0 
            }).ToList();

            return await orderRepository.CreateOrderAsync(order, orderDetails);
        }
    }
}
