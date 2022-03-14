using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Data.Model
{
    public class Transactions
    {
        [Key]
        [Required]
        public Guid TransactionID { get; set; } 
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string? TransactionDesc { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string? FromPayee { get; set; }


    }
}
