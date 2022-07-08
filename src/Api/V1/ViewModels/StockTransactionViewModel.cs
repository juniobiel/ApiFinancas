using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class StockTransactionViewModel
    {
        [Key]
        public Guid? StockTransactionId { get; set; }
        public int? StockId { get; set; }

        [Required(ErrorMessage = "O Ticker precisa ser informado")]
        public string StockTicker { get; set; }

        [Required(ErrorMessage = "Insira o valor da ação")]
        public decimal StockPrice { get; set; }

        [Required(ErrorMessage = "O valor precisa ser maior que 0")]
        [Range(1, 9999)]
        public int StockQt { get; set; }

        [Required(ErrorMessage = "O valor precisa ser maior que 0")]
        [Range(0.01, 9999)]
        public decimal TransactionTaxes { get; set; }

        [Required(ErrorMessage = "Insira a data da compra")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "É necessário selecionar se é uma compra ou uma venda")]
        [Range(3,4)]
        public int TransactionType { get; set; }
    }
}
