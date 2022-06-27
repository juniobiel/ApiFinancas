using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class TransactionFilterViewModel
    {
        [Key]
        public Guid? TransactionId { get; set; }
        public Guid? AccountId { get; set; }
        public int? TransactionType { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }

    }
}
