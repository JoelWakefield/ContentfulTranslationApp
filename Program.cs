using System.Text;
using Newtonsoft.Json;

class Program
{
  private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";
  private static readonly string route = "/translate?api-version=3.0&from=en-US&to=fr-CA&to=es-MX";
  private static readonly string location = "centralus";

  static async Task Main(string[] args)
  {
    string key = args[0];
    // Input and output languages are defined as parameters.
    string path = Path.Combine(Directory.GetCurrentDirectory(), "content.json");
    Content[] contents = JsonConvert.DeserializeObject<Content[]>(File.ReadAllText(path))!;

    List<object> body = new List<object>();
    foreach (var content in contents)
    {
      body.Add(new { Text = content.Text });
    }

    var requestBody = JsonConvert.SerializeObject(body);

    using (var client = new HttpClient())
    using (var request = new HttpRequestMessage())
    {
      // Build the request.
      request.Method = HttpMethod.Post;
      request.RequestUri = new Uri(endpoint + route);
      request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
      request.Headers.Add("Ocp-Apim-Subscription-Key", key);
      request.Headers.Add("Ocp-Apim-Subscription-Region", location);

      // Send the request and get response.
      HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
      // Read response as a string.
      string result = await response.Content.ReadAsStringAsync();
      List<TranslationSet> sets = JsonConvert.DeserializeObject<List<TranslationSet>>(result)!;

      foreach (TranslationSet set in sets)
      {
        set.Translations?.ForEach((t) => Console.WriteLine(t.Text));
      }

      File.WriteAllText(Path.Combine(
        Directory.GetCurrentDirectory(), "translations.json"),
        result
      );
    }
  }
}