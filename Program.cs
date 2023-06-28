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

    List<TranslationSet> sets = new List<TranslationSet>();

    foreach (var content in contents)
    {
      using (var client = new HttpClient())
      using (var request = new HttpRequestMessage())
      {
        object[] body = new object[] { new { Text = content.Text } };
        var reqBody = JsonConvert.SerializeObject(body);

        // Build the request.
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(endpoint + route);
        request.Content = new StringContent(reqBody, Encoding.UTF8, "application/json");
        request.Headers.Add("Ocp-Apim-Subscription-Key", key);
        request.Headers.Add("Ocp-Apim-Subscription-Region", location);

        // Send the request and get response.
        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
        // Read response as a string.
        string result = await response.Content.ReadAsStringAsync();
        MSTranslationSet[] msSet = JsonConvert.DeserializeObject<MSTranslationSet[]>(result)!;

        TranslationSet set = new TranslationSet();
        set.Translations = new List<Translation>();
        set.Translations?.Add(new Translation
        {
          Text = content.Text,
          Lang = "en-US",
        });

        foreach (var translation in msSet[0].Translations!)
        {
          set.Translations?.Add(new Translation
          {
            Text = translation.Text,
            Lang = translation.To
          });
        }
        sets.Add(set);
      }
    }

    File.WriteAllText(Path.Combine(
      Directory.GetCurrentDirectory(), "translations.json"),
      JsonConvert.SerializeObject(sets)
    );
  }
}