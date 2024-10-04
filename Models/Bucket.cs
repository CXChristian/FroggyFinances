using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    public class Bucket
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Company { get; set; }

        public ICollection<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    }
}