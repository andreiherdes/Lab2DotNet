using System;
using System.Collections.Generic;

namespace ClassLibrary1
{
    public class Product
    {
        private int id;
        private List<PriceHistory> priceHistoryList;
        private DateTime startDate;
        private DateTime endDate;

        public Product(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new BusinessException("Product is not valid!");
            }
            priceHistoryList = new List<PriceHistory>();
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get => id; set => id = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public List<PriceHistory> PriceHistoryList { get => priceHistoryList; set => priceHistoryList = value; }

        public void AddPrice(double productPrice)
        {
            if (EndDate < DateTime.Now)
            {
                throw new BusinessException("Product is not valid");
            }

            if (priceHistoryList.Count == 0)
            {
                Price newPrice = new Price(DateTime.Now, productPrice);
                priceHistoryList.Add(new PriceHistory(newPrice));
            }
            else
            {
                PriceHistory priceHistory = priceHistoryList[priceHistoryList.Count - 1];
                priceHistory.Price.EndDate = DateTime.Now;
                double currentPrice = priceHistory.Price.ProductPrice;

                if (currentPrice < productPrice)
                {
                    Price newPrice = new Price(DateTime.Now, productPrice);
                    priceHistoryList.Add(new PriceHistory(newPrice));
                }
                else
                {
                    throw new BusinessException("New price smaller than current price");
                }
            }
        }

        public int SeePriceHistory()
        {
            if (EndDate < DateTime.Now)
            {
                throw new BusinessException("Product is not valid");
            }
            int numberOfPriceHistory = 0;
            foreach (PriceHistory priceHistory in PriceHistoryList)
            {
                numberOfPriceHistory += 1;
                Console.WriteLine("Product price is {0}, valid from {1} until {2}",
                    priceHistory.Price.ProductPrice, priceHistory.Price.StartDate, priceHistory.Price.EndDate);
            }

            return numberOfPriceHistory;
        }

        public void SeePriceHistoryInInterval(DateTime starDate, DateTime endDate)
        {
            if (EndDate < DateTime.Now)
            {
                throw new BusinessException("Product is not valid");
            }
            foreach (PriceHistory priceHistory in PriceHistoryList)
            {
                if (priceHistory.Price.StartDate >= starDate && priceHistory.Price.EndDate <= endDate)
                {
                    Console.WriteLine("Product price is {0}, valid from {1} until {2}",
                        priceHistory.Price.ProductPrice, priceHistory.Price.StartDate, priceHistory.Price.EndDate);
                }
            }
        }

        public double GetCurrentPrice()
        {
            if (PriceHistoryList.Count == 0)
            {
                throw new BusinessException("Product has no price");
            }

            return PriceHistoryList[PriceHistoryList.Count - 1].Price.ProductPrice;
        }
    }
}
