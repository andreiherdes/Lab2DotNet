using System;
using ClassLibrary1;
using Xunit;

namespace XUnitTestProject1
{
    public class ProductTest
    {
        [Fact]
        public void GivenDataForProductWhenDataIsValidThenInitializeProduct()
        {
            Product testProduct = new Product(new DateTime(2016,6,21), new DateTime(2017,1,10));

            Assert.NotNull(testProduct);
        }

        [Fact]
        public void GivenDataForProductWhenDataIsNotValidThenExceptionIsThrown()
        {
            Assert.Throws<BusinessException>(() => new Product(new DateTime(2018, 6, 21), new DateTime(2017, 1, 10)));
        }

        [Fact]
        public void GivenAProductWhenProductInvalidThenExceptionIsThrown()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2017, 1, 10));

            Assert.Throws<BusinessException>(() => testProduct.AddPrice(20));
        }

        [Fact]
        public void GivenAPriceWhenProductIsValidThenAddPriceToPriceHistory()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            testProduct.AddPrice(50);

            Assert.True(testProduct.PriceHistoryList.Count == 1);
        }

        [Fact]
        public void GivenAPricenWhenProductIsValidAndAlreadyContainsAPriceThenUpdateCurrentPrice()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            double firstPrice = 20;
            double secondPrice = 50;
            testProduct.AddPrice(firstPrice);
            testProduct.AddPrice(secondPrice);

            Assert.True((int)testProduct.GetCurrentPrice() == (int)secondPrice);
        }

        [Fact]
        public void GivenAPriceWhenProductIsValidAndAlreadyContainsAPriceThenSetEndDateForPreviousPrice()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            double firstPrice = 20;
            double secondPrice = 50;
            testProduct.AddPrice(firstPrice);
            testProduct.AddPrice(secondPrice);

            Price priceToBeTested = testProduct.PriceHistoryList[0].Price;
            DateTime endDate = priceToBeTested.EndDate.Value;

            Assert.True(endDate.Date == DateTime.Now.Date);
        }

        [Fact]
        public void GivenASmallerPriceWhenProductIsValidThenThrowException()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            double firstPrice = 20;
            double secondPrice = 10;

            testProduct.AddPrice(firstPrice);

            Assert.Throws<BusinessException>(() => testProduct.AddPrice(secondPrice));
        }

        [Fact]
        public void GivenABiggerPriceWhenProductIsValidUpdatePrice()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            double firstPrice = 20;
            double secondPrice = 50;

            testProduct.AddPrice(firstPrice);
            testProduct.AddPrice(secondPrice);

            Assert.True((int)testProduct.GetCurrentPrice() == (int)secondPrice);
        }

        [Fact]
        public void GivenAProductWhenValidThenSeeHistory()
        {
            Product testProduct = new Product(new DateTime(2017, 1, 5), new DateTime(2018, 1, 10));
            double firstPrice = 20;
            double secondPrice = 50;
            double thirdPrice = 60;

            testProduct.AddPrice(firstPrice);
            testProduct.AddPrice(secondPrice);
            testProduct.AddPrice(thirdPrice);

            int numberOfPriceHistory = testProduct.SeePriceHistory();

            Assert.True(numberOfPriceHistory == 3);
        }

        [Fact]
        public void GivenATimeIntervalWhenProductValidThenSeeFilteredHistory()
        {
            Product testProduct = new Product(new DateTime(2016,1,5), new DateTime(2018,1,10));

            double firstPrice = 20;
            double secondPrice = 50;
            double thirdPrice = 60;

            testProduct.AddPrice(firstPrice);
            testProduct.AddPrice(secondPrice);
            testProduct.AddPrice(thirdPrice);

            DateTime outOfTimeZone = new DateTime(2020,10,1);
            DateTime starDate = new DateTime(2016,10,1);
            DateTime endDate = new DateTime(2017,10,1);

            testProduct.PriceHistoryList[0].Price.StartDate = outOfTimeZone;

            int numberOfFilteredPrices = testProduct.SeePriceHistoryInInterval(starDate, endDate);

            Assert.True(numberOfFilteredPrices == 2);
        }
    }
}
