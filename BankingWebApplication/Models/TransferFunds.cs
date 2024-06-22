using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankingWebApplication.Models
{
    public class TransferFunds
    {
        [Required]
        [Display(Name = "Sender Account Number")]
        public string SenderAccountNumber { get; set; }

        [Required]
        [Display(Name = "Receiver Account Number")]
        public string ReceiverAccountNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

    }
}