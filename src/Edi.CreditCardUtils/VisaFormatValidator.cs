namespace Edi.CreditCardUtils
{
    public class VisaFormatValidator : ICreditCardBrandFormatValidator
    {
        public string BrandName => "Visa";
        public string BrandRegEx => "^4[0-9]{12}(?:[0-9]{3})?$";
    }
}