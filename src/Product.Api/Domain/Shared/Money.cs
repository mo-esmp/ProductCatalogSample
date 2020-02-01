using Product.Api.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Product.Api.Domain.Shared
{
    public class Money : IEquatable<Money>
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

        public decimal Amount { get; protected set; }

        public Currency Currency { get; protected set; }

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

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Equals(Currency, other.Currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Money)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public static bool operator ==(Money value1, Money value2)
        {
            return EqualityComparer<Money>.Default.Equals(value1, value2);
        }

        public static bool operator !=(Money value1, Money value2)
        {
            return !(value1 == value2);
        }
    }
}