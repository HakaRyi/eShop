using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task SaveAsync();
        Task<bool> ExistsTransactionForOrderAsync(int orderId);
    }
}
