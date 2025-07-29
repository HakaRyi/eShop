using BOs;
using BOs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly EShopContext _context;

        public TransactionRepository(EShopContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsTransactionForOrderAsync(int orderId)
        {
            return await _context.Transactions.AnyAsync(t => t.OrderId == orderId);
        }

    }
}
