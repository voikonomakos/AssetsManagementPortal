public class ExchangeRates
{
    public string Currency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime Timestamp { get; set; }

    public ExchangeRates(string currency, decimal rate, DateTime timestamp)
    {
        Currency = currency;
        Rate = rate;
        Timestamp = timestamp;
    }
}
