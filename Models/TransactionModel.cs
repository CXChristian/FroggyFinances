using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    [Table("Transactions")]
    public class TransactionModel
    {
        [Key]
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Company { get; set; }
        public float Amount { get; set; }
        public string? BucketCategory { get; set; }  = "Uncategorized";  //default value
    }
}