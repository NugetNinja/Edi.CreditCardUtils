using System;
using System.Collections.Generic;
using System.Text;

namespace Edi.CreditCardUtils.BINValidators
{
    public class UnionPayCardValidator : IBINFormatValidator
    {
        public string BrandName => "Union Pay";
        public string BrandRegEx => "^((62|81)[0-9]{14,17})$";
    }
}
