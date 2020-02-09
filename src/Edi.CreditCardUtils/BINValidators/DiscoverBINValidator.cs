using System;
using System.Collections.Generic;
using System.Text;

namespace Edi.CreditCardUtils.BINValidators
{
    public class DiscoverBINValidator : IBINFormatValidator
    {
        public string BrandName => "Discover";

        public string BrandRegEx => "^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$";
    }
}
