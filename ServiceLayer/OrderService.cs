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
        private readonly OrderRepository _orderRepository;
        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDTO> GetPendingOrderByMemberIdAsync(int memberId)
        {
            var order = await _orderRepository.GetPendingOrderByMemberIdAsync(memberId);
            if (order != null && order.Status == OrderStatus.Pending)
            {
               
                decimal total = order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity);
                order.Freight = total * 0.1m > 5m ? total * 0.1m : 5m;
                await _orderRepository.UpdateOrderAsync(order);
            }
            return MapToOrderDTO(order);
        }

     
        public async Task AddToCartAsync(int memberId, int productId, decimal unitPrice)
        {
            var order = await _orderRepository.GetPendingOrderByMemberIdAsync(memberId);
            var existingDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == productId);

            if (existingDetail != null)
            {
                existingDetail.Quantity += 1; 
                await _orderRepository.UpdateOrderDetailAsync(existingDetail);
            }
            else
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    UnitPrice = unitPrice,
                    Quantity = 1,
                    Discount = 0
                };
                await _orderRepository.AddOrderDetailAsync(orderDetail);
            }
        }

      
        public async Task UpdateCartAsync(int orderDetailId, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than 0");
            var orderDetail = await _orderRepository.FindOrderDetailAsync(orderDetailId);
            if (orderDetail != null)
            {   
                orderDetail.Quantity = quantity;
                await _orderRepository.UpdateOrderDetailAsync(orderDetail);
            }
        }

        
        public async Task DeleteFromCartAsync(int orderDetailId)
        {
            await _orderRepository.DeleteOrderDetailAsync(orderDetailId);
        }

       
        public async Task CheckoutAsync(int orderId, DateTime? requiredDate)
        {
            var order = await _orderRepository.FindOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with ID {orderId} not found.");
            }

         
            decimal total = order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity);
            order.Freight = total * 0.1m > 5m ? total * 0.1m : 5m;

         
            order.RequiredDate = requiredDate;

            
            await _orderRepository.UpdateOrderStatusAsync(orderId, OrderStatus.Success);
        }
        public async Task<List<OrderDetailDTO>> GetOrderHistoryByMemberIdAsync(int memberId)
        {
            var orderDetails = await _orderRepository.GetOrderDetailsByMemberIdAsync(memberId);
            return orderDetails.Select(od => new OrderDetailDTO
            {
                OrderDetailId = od.OrderDetailId,
                ProductId = od.ProductId,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Discount = od.Discount,
                ProductName = od.Product?.ProductName,
                OrderId = od.OrderId, 
                OrderDate = od.Order.OrderDate 
            }).ToList();
        }
        // Map Order sang DTO
        private OrderDTO MapToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate, 
                ShippedDate = order.ShippedDate, 
                Freight = order.Freight,
                Status = order.Status,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    ProductId = od.ProductId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount,
                    ProductName = od.Product?.ProductName,
                    OrderId = od.OrderId,
                    OrderDate = od.Order.OrderDate
                }).ToList()
            };
        }

    }
}
