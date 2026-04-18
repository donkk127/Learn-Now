using LearnNow.web.Controllers;

namespace LearnNow.Test
{
    public class PaymentServiceTests
    {
        [Fact]
        public void CalculateChange_WhenPaymentGreaterThanCost_ReturnsCorrectChange()
        {
            //Arrange
            var service=new PaymentService();
            decimal courseCost = 100m;
            decimal amountPaid = 150m;

            //Act 
            decimal result=service.CalculateChange(courseCost, amountPaid);

            //Assert
            Assert.Equal(50m, result);
        }

        [Fact]
        public void CalculateChange_WhenPaymentEqualToCost_ReturnsZero()
        {
            var service=new PaymentService();
            decimal result = service.CalculateChange(200m, 200m);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void CalculateChange_WhenPaymentLessThanCost_ThrowsArgumentException()
        {
            var service = new PaymentService();

            var exception = Assert.Throws<ArgumentException>(() => service.CalculateChange(300m, 250m));

            Assert.Equal("El monto pagado es insuficiente. ", exception.Message);
        }
    }
}