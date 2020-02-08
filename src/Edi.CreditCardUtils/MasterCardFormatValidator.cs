﻿namespace Edi.CreditCardUtils
{
    public class MasterCardFormatValidator : ICreditCardBrandFormatValidator
    {
        public string BrandName => "MasterCard";
        public string BrandRegEx =>
            "^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$";
    }
}