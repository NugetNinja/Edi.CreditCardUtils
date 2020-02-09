namespace Edi.CreditCardUtils.BINValidators
{
    public class VisaBINValidator : IBINFormatValidator
    {
        public string BrandName => "Visa";
        public string BrandRegEx => "^4[0-9]{12}(?:[0-9]{3})?$";
    }
}