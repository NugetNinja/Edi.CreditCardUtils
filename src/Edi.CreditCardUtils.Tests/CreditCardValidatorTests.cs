using System.Collections.Generic;
using Edi.CreditCardUtils.BINValidators;
using NUnit.Framework;

namespace Edi.CreditCardUtils.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Invalid_BadStringFormat);
        }

        [Test]
        public void TestLuhnMod10Failure()
        {
            var result = CreditCardValidator.ValidCardNumber("9962514040073500");
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Invalid_LuhnFailure);
        }

        [Test]
        public void TestLuhnMod10Success_NoBrand()
        {
            var result = CreditCardValidator.ValidCardNumber("6011000990139424");
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_LuhnOnly);
        }

        [Test]
        public void TestLuhnMod10Success_UnknownBrand()
        {
            var result = CreditCardValidator.ValidCardNumber("4012888888881881", new List<IBINFormatValidator>
            {
                new MasterCardBINValidator()
            });
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_LuhnOnly);
        }

        [Test]
        public void TestValidVisa()
        {
            var result = CreditCardValidator.ValidCardNumber("4012888888881881", new List<IBINFormatValidator>
            {
                new VisaBINValidator(),
                new MasterCardBINValidator()
            });
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_BrandTest && result.CardType == "Visa");
        }

        [Test]
        public void TestValidMasterCard()
        {
            var result = CreditCardValidator.ValidCardNumber("5105105105105100", new List<IBINFormatValidator>
            {
                new VisaBINValidator(),
                new MasterCardBINValidator()
            });
            Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_BrandTest && result.CardType == "MasterCard");
        }
    }
}