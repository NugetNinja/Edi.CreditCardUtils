using System.Collections.Generic;
using System.Linq;
using Edi.CreditCardUtils.BINValidators;
using NUnit.Framework;

namespace Edi.CreditCardUtils.Tests
{
    [TestFixture]
    public class Tests
    {
        public static IEnumerable<TestCaseData> InvalidCardNumbersData
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData("");
                yield return new TestCaseData(" ");
                yield return new TestCaseData("123");
                yield return new TestCaseData("abc");
                yield return new TestCaseData("123abc");
                yield return new TestCaseData("abc123");
                yield return new TestCaseData("1111 2222");
            }
        }

        [TestCaseSource("InvalidCardNumbersData")]
        public void TestInvalidCardNumberFormat(string cardNumber)
        {
            var result = CreditCardValidator.ValidCardNumber(cardNumber);
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Invalid_BadStringFormat);
        }

        [Test]
        public void TestLuhnMod10Failure()
        {
            var result = CreditCardValidator.ValidCardNumber("9962514040073500");
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Invalid_LuhnFailure);
        }

        [Test]
        public void TestLuhnMod10Success()
        {
            var result = CreditCardValidator.ValidCardNumber("214916279426729");
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_LuhnOnly);
        }

        [Test]
        public void TestValidVisa()
        {
            var result = CreditCardValidator.ValidCardNumber("4012888888881881");
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_BINTest && result.CardTypes.Contains("Visa"));
        }

        [Test]
        public void TestValidMasterCard()
        {
            var result = CreditCardValidator.ValidCardNumber("5105105105105100");
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_BINTest && result.CardTypes.Contains("MasterCard"));
        }

        public class WellsFargoBankValidator : ICardTypeValidator
        {
            public string Name => "Wells Fargo Bank";
            public string RegEx => @"^(485246)\d{10}$";
        }

        [Test]
        public void TestCardTypeValidator()
        {
            var result = CreditCardValidator.ValidCardNumber("4852461030260066", new ICardTypeValidator[]
            {
                new WellsFargoBankValidator()
            });
            Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_BINTest && result.CardTypes.Contains("Wells Fargo Bank"));
        }
    }
}