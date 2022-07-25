using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using Xunit;

namespace UnitTests
{
    public class StockPurchaseServiceTests
    {
        private readonly StockPurchaseService _service;
        private readonly Mock<IStockPurchaseRepository> _stockPurchaseRepository;
        public StockPurchaseServiceTests()
        {
            _stockPurchaseRepository = new Mock<IStockPurchaseRepository>();
            _service = new StockPurchaseService(_stockPurchaseRepository.Object);
        }

        [Fact(DisplayName = "Cadastrar a transação e retornar status code 201")]
        [Trait("Fluxo", "Sucesso")]
        public async Task ReturnStatusCode201_AtRegisterNewStockPurchase()
        {
            //Arrange
            PurchaseData purchase = new PurchaseData();
            _stockPurchaseRepository.Setup(x => x.AddNewPurchase(purchase))
                .Returns(Task.FromResult(true));

            //Act
            var result = await _service.NewPurchase(purchase);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal<int>(201, result);
        }


    }

    
}
