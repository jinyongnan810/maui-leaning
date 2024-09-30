using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    private readonly HttpClient httpClient;
    public MonkeyService()
    {
        httpClient = new HttpClient();
    }
    public async Task<List<Monkey>?> GetMonkeys()
    {
        var response = await httpClient.GetAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            return content;
        }
        return null;
    }
}
