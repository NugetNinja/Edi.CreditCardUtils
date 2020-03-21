using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Edi.CreditCardUtils
{
    public class CreditCardGenerator
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

		public static string GenerateCardNumber(string bin, int length)
        {
            int[] digits = new int[length];
            var prefixDigits = bin.Select(p => p - '0').ToArray();

            for (var i = 0; i < prefixDigits.Length; i++)
            {
                digits[i] = prefixDigits[i];
            }

            for (var i = bin.Length; i < length - 1; i++)
            {
                var digit = Random.Value.Next(0, 10);
                digits[i] = digit;
            }

            digits[length - 1] = Luhn.GenerateCheckDigit(digits[..(length -1)]);
            return string.Join(null, digits);
        }
	}
}