using System.ComponentModel.DataAnnotations;

namespace Api.V2.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "É necessário informar um nome para a categoria")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "É necessário informar qual o tipo de transação")]
        public int TransactionType { get; set; }
        public Guid? CategoryCreatedByUserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CategoryUpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
