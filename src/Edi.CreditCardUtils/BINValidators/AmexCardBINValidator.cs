using System;
using System.Collections.Generic;
using System.Text;

namespace Edi.CreditCardUtils.BINValidators
{
    public class AmexCardBINValidator : IBINFormatValidator
    {
        public string BrandName => "Amex Card";
        public string BrandRegEx => "^3[47][0-9]{13}$";
    }
}
