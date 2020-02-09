namespace Edi.CreditCardUtils.BINValidators
{
    public interface ICardTypeValidator
    {
        string Name { get; }
        string RegEx { get; }
    }
}
