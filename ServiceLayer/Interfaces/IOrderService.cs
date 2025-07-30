using BOs.Entities;
using RepositoryLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAllOrder();
        Order GetOrder(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);

        List<SalesReportItem> GetSalesReport(DateTime start, DateTime end);
    }
}
