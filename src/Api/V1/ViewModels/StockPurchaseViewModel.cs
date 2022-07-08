using System.ComponentModel.DataAnnotations;

namespace Api.V1.ViewModels
{
    public class StockPurchaseViewModel
    {
        [Key]
        public Guid? StockPurchaseId { get; set; }
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
        public decimal PurchaseTaxes { get; set; }

        [Required(ErrorMessage = "Insira a data da compra")]
        public DateTime PurchaseDate { get; set; }
    }
}
