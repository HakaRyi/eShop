using BOs.Entities;
using RepositoryLayer.Models; 
using System;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrder();
        Order GetOrder(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);

        // Sửa kiểu trả về
        List<SalesReportItem> GetSalesReport(DateTime start, DateTime end);
    }
}
