using System.Text.Json;
using RestSharp;

public class ExchangeRatesService
{
    private readonly string _appId;

    private readonly string _url;

    public ExchangeRatesService(string appId, string url)
    {
        _appId = appId;
        _url = url;
    }

    public async Task<Dictionary<string, decimal>> FetchERAsync()
    {
        var client = new RestClient(_url);
        var request = new RestRequest();

        request.AddParameter("app_id", _appId);

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("Failed to fetch data from API.");

        if (string.IsNullOrEmpty(response.Content))
            throw new Exception("API returned empty response.");

        var json = JsonDocument.Parse(response.Content);

        var rates = json.RootElement.GetProperty("rates");

        var result = new Dictionary<string, decimal>();

        foreach (var rate in rates.EnumerateObject())
        {
            var key = rate.Name;
            var value = rate.Value.GetDecimal();

            result[key] = value;
        }

        return result;
    }
}