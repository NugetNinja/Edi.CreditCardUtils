using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Edi.CreditCardUtils
{
    public class CreditCardValidator
    {
        public static CreditCardValidationResult ValidCardNumber(
            string cardNumber, IEnumerable<ICreditCardBrandFormatValidator> formatValidators = null)
        {
            CreditCardValidationResult CreateResult(CreditCardNumberFormat format, string cardType = null)
            {
                return new CreditCardValidationResult
                {
                    CreditCardNumberFormat = format,
                    CardType = cardType
                };
            }

            // Check card number length
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16)
            {
                return CreateResult(CreditCardNumberFormat.Invalid_BadStringFormat);
            }

            // Check string is all numbers
            var isMatch = Regex.IsMatch(cardNumber, @"^\d*$");
            if (!isMatch)
            {
                return CreateResult(CreditCardNumberFormat.Invalid_BadStringFormat);
            }

            // Try Luhn Test
            var digits = GetDigitsArrayFromCardNumber(cardNumber);
            if (!IsLuhnValid(digits))
            {
                return CreateResult(CreditCardNumberFormat.Invalid_LuhnFailure);
            }

            if (null == formatValidators) return CreateResult(CreditCardNumberFormat.Valid_LuhnOnly);

            var creditCardBrandFormatValidators = formatValidators as ICreditCardBrandFormatValidator[] ?? formatValidators.ToArray();
            if (!creditCardBrandFormatValidators.Any())
            {
                return CreateResult(CreditCardNumberFormat.Valid_LuhnOnly);
            }

            // Test against brand validator
            foreach (var validator in creditCardBrandFormatValidators)
            {
                var brandMatch = Regex.IsMatch(cardNumber, validator.BrandRegEx);
                if (brandMatch)
                {
                    return CreateResult(CreditCardNumberFormat.Valid_BrandTest, validator.BrandName);
                }
            }

            // No brand matches, but still a valid Luhn
            return CreateResult(CreditCardNumberFormat.Valid_LuhnOnly);
        }

        /// <summary>
        /// Check credit card numbers agaist Luhn Algorithm
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="digits">Credit card numbers</param>
        /// <returns>Is valid Luhn</returns>
        public static bool IsLuhnValid(int[] digits)
        {
            var sum = 0;
            var alt = false;
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                if (alt)
                {
                    digits[i] *= 2;
                    if (digits[i] > 9)
                    {
                        digits[i] -= 9;
                    }
                }
                sum += digits[i];
                alt = !alt;
            }

            return sum % 10 == 0;
        }

        public static int[] GetDigitsArrayFromCardNumber(string cardNumber)
        {
            var digits = cardNumber.Select(p => int.Parse(p.ToString())).ToArray();
            return digits;
        }
    }
}
