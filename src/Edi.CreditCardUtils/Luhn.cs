using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edi.CreditCardUtils
{
    public class Luhn
    {
        /// <summary>
        /// Check credit card numbers agaist Luhn Algorithm
        /// https://en.wikipedia.org/wiki/Luhn_algorithm
        /// </summary>
        /// <param name="digits">Credit card numbers</param>
        /// <returns>Is valid Luhn</returns>
        public static bool IsLuhnValid(int[] digits)
        {
            var sum = CalculateSum(digits, 1);
            return sum % 10 == 0;
        }

        /// <summary>
        /// Generate check digit to make the digits pass Luhn
        /// </summary>
        /// <param name="digits">Digit bits without tailing check digit</param>
        /// <returns>Check digit</returns>
        public static int GenerateCheckDigit(int[] digits)
        {
            var sum = CalculateSum(digits);

            var lastDigit = sum * 9 % 10;
            return lastDigit;
        }

        public static int[] GetDigitsArrayFromCardNumber(string cardNumber)
        {
            var digits = cardNumber.Select(p => p - '0').ToArray();
            return digits;
        }

        private static int CalculateSum(int[] digits, int bitShift = 0)
        {
            var sum = digits.Reverse()
                                .Select((digit, i) =>
                                        (i + bitShift) % 2 == 0
                                        ? digit * 2 > 9 ? digit * 2 - 9 : digit * 2
                                        : digit)
                                .Sum();
            return sum;
        }
    }
}
