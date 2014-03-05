using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kata
{
    [TestFixture]
    public class GivenCheckout
    {
        [Test]
        public void ScanNoItems_TotalIsZero()
        {
            var checkout = new Checkout(new PriceList());
            Assert.AreEqual(0, checkout.Total());
        }
        
        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void ScanSingleItems_ReturnsCorrectPrice(string sku, int expectedPrice)
        {
            var checkout = new Checkout(new PriceList());
            checkout.Scan(sku);
            Assert.AreEqual(expectedPrice, checkout.Total());
        }

        [Test]
        public void ScanMultipleItems_ReturnsCorrectPrice()
        {
            var checkout = new Checkout(new PriceList());
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.AreEqual(80, checkout.Total());
        }

        [Test]
        public void ScanMultipleItemsWithSpecialOffer_ReturnsCorrectPrice()
        {
            var checkout = new Checkout(new PriceList());
            checkout.Scan("B");
            checkout.Scan("B");
            Assert.AreEqual(45, checkout.Total());
        }

        [Test]
        public void ScanThreeProductCtems_ReturnsCorrectPrice()
        {
            var checkout = new Checkout(new PriceList());
            checkout.Scan("C");
            checkout.Scan("C");
            checkout.Scan("C");
            Assert.AreEqual(60, checkout.Total());
        }
    }

    public class PriceList
    {
        private readonly Dictionary<string, int> _prices;

        public PriceList()
        {
            _prices = new Dictionary<string, int>
                {
                    {"A", 50},
                    {"B", 30},
                    {"C", 20},
                    {"D", 15}
                };
        }

        public int GetPriceList(string sku)
        {
            return _prices[sku];
        }
    }

    public class Checkout
    {
        private readonly PriceList _priceList;

        public Checkout(PriceList priceList)
        {
            _priceList = priceList;
        }

        private List<string> _scannedItems;

        public int Total()
        {
            int totalPrice = 0;
            foreach (var item in _scannedItems)
            {
                totalPrice += _priceList.GetPriceList(item);
            }

            if (_scannedItems.Select(x => x == "B").Count() == 2)
            {
                totalPrice -= 15;
            }

            return totalPrice;
        }

        public void Scan(string sku)
        {
            _scannedItems.Add(sku);
        }
    }
}

