using BOs.Entities;
using RepositoryLayer;
using System;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TransactionService
    {
        private readonly ITransactionRepository _txRepo;
        private readonly OrderRepository _orderRepo;

        public TransactionService(ITransactionRepository txRepo, OrderRepository orderRepo)
        {
            _txRepo = txRepo;
            _orderRepo = orderRepo;
        }

        public async Task CreateTransactionAsync(int orderId, decimal amount, string note = "")
        {
            // 🔎 Check xem transaction đã tồn tại chưa
            var existing = await _txRepo.ExistsTransactionForOrderAsync(orderId);
            if (existing)
                return;

            // ✅ Lấy MemberId từ Order
            var order = await _orderRepo.FindOrderByIdAsync(orderId);
            int? memberId = order?.MemberId;

            var transaction = new Transaction
            {
                OrderId = orderId,
                MemberId = memberId,
                TransactionDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = "PayOS",
                Status = "PAID",
                Note = note
            };

            await _txRepo.AddAsync(transaction);
            await _txRepo.SaveAsync();
        }
    }
}
