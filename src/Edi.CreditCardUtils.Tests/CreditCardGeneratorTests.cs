using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Edi.CreditCardUtils.Tests
{
    public class CreditCardGeneratorTests
    {
        [Test]
        public void TestGenerateCardNumber()
        {
            var bin = "485246";
            int length = 16;
            var cn = CreditCardGenerator.GenerateCardNumber(bin, length);

            Assert.IsNotEmpty(cn);
            Assert.IsTrue(cn.Length == length);

            var result = CreditCardValidator.ValidCardNumber(cn);

            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_LuhnOnly
                          || result.CardNumberFormat == CardNumberFormat.Valid_BINTest);
        }

        [Test]
        public void TestGenerateBatchCardNumbers()
        {
            var bin = "485246";
            int length = 16;

            var cardNumbers = new List<string>();
            for (int i = 0; i < 128; i++)
            {
                var cn = CreditCardGenerator.GenerateCardNumber(bin, length);
                cardNumbers.Add(cn);
            }

            var isUnique = cardNumbers.GroupBy(x => x).All(g => g.Count() == 1);
            Assert.IsTrue(isUnique);
        }
    }
}
