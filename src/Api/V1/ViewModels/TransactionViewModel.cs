using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class TransactionViewModel
    {
        [Key]
        public Guid? TransactionId { get; set; }

        [Required(ErrorMessage = "Informe a conta vinculada à operação")]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Informe um tipo válido de transação")]
        public int TransactionType { get; set; }

        [Required(ErrorMessage = "Informe uma categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "É necessário um valor mínimo de R$0,01 para efetuar uma transação")]
        public decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Descreva a operação")]
        public string Description { get; set; }

        public Guid? AccountReceiverId { get; set; }
        public Guid? TransactionCreatedByUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? TransactionUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
