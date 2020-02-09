# Edi.CreditCardUtils
.NET Standard Lib for Credit Card Number Operations

> This project is still in early development, it comes with !!!ABSOLUTELY NO WARRANT!!! Please review the code before using it in your project.

## Usage

### Install from NuGet

Powershell

```powershell
Install-Package Edi.CreditCardUtils -Version 0.2.0-alpha
```

.NET Core CLI
```bash
dotnet add package Edi.CreditCardUtils --version 0.2.0-alpha
```

### Validate Credit Card Number

The ```CreditCardValidator``` does 3 things:

1. Check card number format (is 14-16 digits)
2. Perform Luhn check (Mod10)
3. (Optional) Test with brand regex (Visa / Master or customized provider)

Return Type:

```csharp
public class CreditCardValidationResult
{
    public CreditCardNumberFormat CreditCardNumberFormat { get; set; }
    public string CardType { get; set; }
}

public enum CreditCardNumberFormat
{
    None = 0,
    Valid_LuhnOnly = 100,
    Valid_BrandTest = 101,
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

Validate a Visa card with 2 card brands

```csharp
var result = CreditCardValidator.ValidCardNumber("4012888888881881", new IBINFormatValidator[]
{
    new VisaBINValidator(),
    new MasterCardBINValidator()
});
Assert.IsTrue(result.CreditCardNumberFormat == CreditCardNumberFormat.Valid_BINTest && result.CardType == "Visa");
```