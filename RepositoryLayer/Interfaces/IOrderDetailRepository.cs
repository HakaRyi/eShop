using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAllOrderDetail();
        OrderDetail GetOrderDetail(int orderId, int productId); // ✔ composite key
        void CreateOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(int orderId, int productId); // ✔ composite key
    }
}
