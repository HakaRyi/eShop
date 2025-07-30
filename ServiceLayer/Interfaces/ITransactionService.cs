using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransaction(int id);
    }
}
