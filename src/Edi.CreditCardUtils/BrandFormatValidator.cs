namespace Edi.CreditCardUtils
{
    public interface ICreditCardBrandFormatValidator
    {
        string BrandName { get; }
        string BrandRegEx { get; }
    }
}
