using Product.Api.Domain.Exceptions;
using System;

namespace Product.Api.Domain.Shared
{
    public class Money
    {
        protected Money()
        {
        }

        protected Money(decimal amount, CurrencyCode currencyCode, ICurrencyLookup currencyLookup)
        {
            var currency = currencyLookup.FindCurrency(currencyCode);
            if (!currency.InUse)
                throw new ArgumentException($"Currency {currencyCode} is not valid");

            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),
                    $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");

            Amount = amount;
            Currency = currency;
        }

        public static Money FromDecimal(decimal amount, CurrencyCode currencyCode, ICurrencyLookup currencyLookup) =>
            new Money(amount, currencyCode, currencyLookup);

        protected Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; internal set; }

        public Currency Currency { get; internal set; }

        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchDomainException(
                    "Cannot sum amounts with different currencies");

            return new Money(Amount + summand.Amount, Currency);
        }

        public Money Subtract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchDomainException(
                    "Cannot subtract amounts with different currencies");

            return new Money(Amount - subtrahend.Amount, Currency);
        }

        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);

        public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

        public override string ToString() => $"{Currency.CurrencyCode} {Amount}";
    }
}