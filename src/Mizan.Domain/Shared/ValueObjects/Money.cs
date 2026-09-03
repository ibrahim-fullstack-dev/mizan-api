namespace Mizan.Domain.Shared.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }

    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException(
                "Currency is required.",
                nameof(currency));

        currency = currency.Trim().ToUpperInvariant();

        if (currency.Length != 3)
            throw new ArgumentException(
                "Currency must be a 3-letter code.",
                nameof(currency));

        return new Money(amount, currency);
    }
}