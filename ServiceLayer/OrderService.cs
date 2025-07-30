using BOs.Entities;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderService : IOrderService
    {
		private readonly IOrderRepository _orderRepository;

		public OrderService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public void CreateOrder(Order order)
        {
            _orderRepository.CreateOrder(order);
        }

        public void DeleteOrder(Order order)
        {
           _orderRepository.DeleteOrder(order);
        }

        public List<Order> GetAllOrder()
        {
            return _orderRepository.GetAllOrder();
        }

        public Order GetOrder(int id)
        {
            return _orderRepository.GetOrder(id);
        }

        public List<SalesReportItem> GetSalesReport(DateTime start, DateTime end)
        {
            return _orderRepository.GetSalesReport(start, end);
        }

        public void UpdateOrder(Order order)
        {
           _orderRepository.UpdateOrder(order);
        }
    }
}
