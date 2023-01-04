using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public string Concept { get; set; } 
        
        [Required]
        public int ToAccountId { get; set; }
    }
}