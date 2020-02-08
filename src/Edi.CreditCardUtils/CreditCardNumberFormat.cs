namespace Edi.CreditCardUtils
{
    public enum CreditCardNumberFormat
    {
        None = 0,
        Valid_LuhnOnly = 100,
        Valid_BrandTest = 101,
        Invalid_BadStringFormat = 200,
        Invalid_LuhnFailure = 201
    }
}