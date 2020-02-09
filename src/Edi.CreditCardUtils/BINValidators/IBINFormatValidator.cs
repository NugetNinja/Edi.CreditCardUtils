namespace Edi.CreditCardUtils.BINValidators
{
    public interface IBINFormatValidator
    {
        string BrandName { get; }
        string BrandRegEx { get; }
    }
}
