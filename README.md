# Edi.CreditCardUtils
.NET Standard Lib for Credit Card Number Operations

> This project is still in early development, it comes with !!!ABSOLUTELY NO WARRANT!!! Please review the code before using it in your project.

## Usage

### Install from NuGet

Powershell

```powershell
Install-Package Edi.CreditCardUtils -Version 0.3.0-alpha
```

.NET Core CLI
```bash
dotnet add package Edi.CreditCardUtils --version 0.3.0-alpha
```

### Validate Credit Card Number

The ```CreditCardValidator``` performs 4 steps:

1. Check card number format (is 14-19 digits)
2. Perform Luhn check (Mod10)
3. Test against known card type regex (Visa / Master etc..)
4. Optional: Test against customized regex

Return Type:

```csharp
public class CreditCardValidationResult
{
    public CardNumberFormat CardNumberFormat { get; set; }
    public string[] CardTypes { get; set; }
}

public enum CardNumberFormat
{
    None = 0,
    Valid_LuhnOnly = 100,
    Valid_BINTest = 101,
    Invalid_BadStringFormat = 200,
    Invalid_LuhnFailure = 201
}
```

#### Examples

> You may find all scenarios in Unit Test code

Validate card number only
```csharp
var result = CreditCardValidator.ValidCardNumber("6011000990139424");
Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_LuhnOnly);
```

Validate a Visa card

```csharp
var result = CreditCardValidator.ValidCardNumber("4012888888881881");
Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_BINTest && result.CardTypes.Contains("Visa"));
```

Customized card validator

```csharp
public class WellsFargoBankValidator : ICardTypeValidator
{
    public string Name => "Wells Fargo Bank";
    public string RegEx => @"^(485246)\d{10}$";
}

[Test]
public void TestCardTypeValidator()
{
    var result = CreditCardValidator.ValidCardNumber("4852461030260066", new ICardTypeValidator[]
    {
        new WellsFargoBankValidator()
    });
    Assert.IsTrue(result.CardNumberFormat == CardNumberFormat.Valid_BINTest && result.CardTypes.Contains("Wells Fargo Bank"));
}
```