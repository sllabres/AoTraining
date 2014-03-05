using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace EvenFibonacci
{
    [TestFixture]
    public class GivenEvenSumCalculator
    {
        [TestCase(0,0)]
        [TestCase(4, 2)]
        [TestCase(7, 10)]
        public void Test(int length, int expectedResult)
        {
            var evenNumbers = new List<int>
                {
                    0,
                    1,
                    1,
                    2,
                    3,
                    5,
                    8,
                    13,
                    21,
                    34,
                };
            Assert.That(new Calculator(evenNumbers).Sum(length), Is.EqualTo(expectedResult));
        }
    }

    public class Calculator
    {      
        private readonly List<int> _evenNumbers;

        public Calculator(List<int> evenNumbers)
        {
            _evenNumbers = evenNumbers;
        }

        public int Sum(int length)
        {
            return _evenNumbers.Take(length).Where(IsEvenNumber).Sum();          
        }

        private static bool IsEvenNumber(int x)
        {
            return x%2 == 0;
        }
    }
}
