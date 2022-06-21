using Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class TransactionViewModel
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required(ErrorMessage = "Informe a conta vinculada à operação")]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Selecione um tipo válido de transação")]
        public TransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "O categoria selecionada é inválida")]
        public int CategoryId { get; set; }

        public decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }
        [Required(ErrorMessage = "Descreva a operação")]
        public string Description { get; set; }

        public Guid? AccountReceiverId { get; set; }
        public Guid TransactionCreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TransactionUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
