using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Company { get; set; }
        public float Amount { get; set; }
    }
}