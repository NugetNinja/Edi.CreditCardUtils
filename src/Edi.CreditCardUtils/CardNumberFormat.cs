namespace Edi.CreditCardUtils
{
    public enum CardNumberFormat
    {
        None = 0,
        Valid_LuhnOnly = 100,
        Valid_BINTest = 101,
        Invalid_BadStringFormat = 200,
        Invalid_LuhnFailure = 201
    }
}