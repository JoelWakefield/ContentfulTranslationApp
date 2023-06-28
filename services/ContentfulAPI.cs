using Contentful.Core;
using Contentful.Core.Models;

public static class ContentfulAPI
{
  public static async Task GetAllEntries()
  {
    // This client should only be created once per application.
    var httpClient = new HttpClient();

    var client = new ContentfulManagementClient(
      httpClient,
      ContentfulSecrets.CONTENTFUL_API_KEY,
      ContentfulSecrets.CONTENTFUL_SPACE_ID
    );

    var entries = await client.GetEntriesCollection<Entry<dynamic>>();

    foreach (var item in entries.Items)
    {
      Console.WriteLine("SYS >> ", item.SystemProperties);
      Console.WriteLine("ID >> ", item.SystemProperties.Id);
      Console.WriteLine("TYPE >> ", item.SystemProperties.Type);
      Console.WriteLine("LINK TYPE >> ", item.SystemProperties.LinkType);
      Console.WriteLine("CONTENT TYPE >> ", item.SystemProperties.ContentType);

      foreach (var field in item.Fields)
      {
        Console.WriteLine(field.ToString());
      }

      Console.WriteLine();
    }
  }
}