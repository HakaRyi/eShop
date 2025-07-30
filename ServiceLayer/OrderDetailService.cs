using BOs.Entities;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class OrderDetailService : IOrderDetailService
    {
		private readonly IOrderDetailRepository _orderDetailRepository;

		public OrderDetailService(IOrderDetailRepository orderDetailRepository)
		{
			_orderDetailRepository = orderDetailRepository;
		}

		public void CreateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.CreateOrderDetail(orderDetail);
        }

        public void DeleteOrderDetail(int orderId, int productId)
        {
            _orderDetailRepository.DeleteOrderDetail(orderId, productId);
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
           return _orderDetailRepository.GetAllOrderDetail();
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _orderDetailRepository.GetOrderDetail(orderId, productId);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.UpdateOrderDetail(orderDetail);
        }
    }
}
