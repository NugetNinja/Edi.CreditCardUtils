using System;
using System.Collections.Generic;
using System.Text;

namespace Edi.CreditCardUtils.BINValidators
{
    public class JCBBINValidator : IBINFormatValidator
    {
        public string BrandName => "JCB";
        public string BrandRegEx => @"^(?:2131|1800|35\d{3})\d{11}$";
    }
}
