using BOs;
using BOs.Entities; 
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly EShopContext _context;

        public TransactionRepository(EShopContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactions() =>
            _context.Transactions.Include(t => t.Order).ToList();

        public Transaction GetTransaction(int id) =>
            _context.Transactions.Include(t => t.Order).FirstOrDefault(t => t.TransactionId == id);
    }
}
