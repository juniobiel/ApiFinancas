using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class AccountViewModel
    {
        [Key]
        public Guid? AccountId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
