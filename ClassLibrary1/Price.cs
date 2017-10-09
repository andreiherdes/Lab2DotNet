using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class Price
    {
        private double productPrice;
        private DateTime startDate;
        private DateTime endDate;

        public Price(DateTime startDate, double productPrice)
        {
            StartDate = startDate;
            ProductPrice = productPrice;
        }

        public Price(DateTime startDate, DateTime endDate, double productPrice)
        {
            StartDate = startDate;
            EndDate = endDate;
            ProductPrice = productPrice;
        }

        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public double ProductPrice { get => productPrice; set => productPrice = value; }
    }
}
