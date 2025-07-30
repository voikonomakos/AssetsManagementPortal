public class JobRunner : IJobRunner
{
    private readonly ExchangeRatesService _service;
    private readonly ExchangeRateRepository _repo;

    public JobRunner(ExchangeRatesService service, ExchangeRateRepository repo)
    {
        _service = service;
        _repo = repo;
    }

    public async Task Run()
    {
        try
        {
            Console.WriteLine($"[{DateTime.Now}] Running scheduled exchange rate job...");
            var rates = await _service.FetchERAsync();
            await _repo.InsertExchangeRates(rates);
            Console.WriteLine("Exchange rates inserted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during job: {ex.Message}");
        }
    }
}
