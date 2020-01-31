namespace Product.Api.Domain.Shared
{
    public class Currency
    {
        public CurrencyCode CurrencyCode { get; set; }

        public bool InUse { get; set; }

        public int DecimalPlaces { get; set; }

        public static Currency None = new Currency { InUse = false };
    }

    public enum CurrencyCode
    {
        Euro,
        Dollar
    }
}