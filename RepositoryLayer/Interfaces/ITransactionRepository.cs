using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransaction(int id);
    }
}
