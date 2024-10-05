using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace expense_transactions.Models
{
    [Table("Buckets")]
    public class Bucket
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Company { get; set; }
    }
}