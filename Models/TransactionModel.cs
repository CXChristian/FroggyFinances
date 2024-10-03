using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    public class TransactionModel
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Date { get; set; }
        [Required]
        public string? Company { get; set; }
        public float Amount { get; set; }
    }
}