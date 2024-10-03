using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    public class Transaction
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Date { get; set; }
        [Required]
        public string? Company { get; set; }
        [Required]
        public float Amount { get; set; }
    }
}