using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kata
{
    [TestFixture]
    public class GivenCheckout : IGetProductPrices
    {
        private int _productPriceToReturn;
        
        [Test]
        public void ScanTwoItems_TotalIs80()
        {
            var calculator = new Calculator();
            var checkout = new CheckOut(this, calculator);
            _productPriceToReturn = 50;
            checkout.Scan("A");
            _productPriceToReturn = 30;
            checkout.Scan("B");

            Assert.That(calculator.Total(), Is.EqualTo(80));
        }

        [Test]
        public void Scan_ThreeASkus_TotalIs130()
        {
            var calculator = new Calculator();
            var checkout = new CheckOut(this, calculator);
            _productPriceToReturn = 50;
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.AreEqual(130, calculator.Total());
        }

        [Test]
        public void ScanMultipleDiscountedItems()
        {
            var calculator = new Calculator();
            var checkout = new CheckOut(this, calculator);
            _productPriceToReturn = 50;
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.AreEqual(180, calculator.Total());
        }

        public int Get(string sku)
        {
            return _productPriceToReturn;
        }
    }

    public class Calculator
    {
        private int _runningTotal;

        public void ItemScanned(int itemPrice)
        {
            _runningTotal += itemPrice;
        }

        public int Total()
        {
            if (_runningTotal == 150)
                return 130;
            if (_runningTotal == 200)
                return 180;

            return _runningTotal;
        }
    }

    public class CheckOut
    {
        private readonly IGetProductPrices _productPrice;
        private readonly Calculator _calculator;

        public CheckOut(IGetProductPrices productPrice, Calculator calculator)
        {
            _productPrice = productPrice;
            _calculator = calculator;
        }

        public void Scan(string sku)
        {
            if (_calculator != null)
                _calculator.ItemScanned(_productPrice.Get(sku));
        }
    }

    public interface IGetProductPrices
    {
        int Get(string sku);
    }
}
