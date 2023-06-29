using Contentful.Core;
using Contentful.Core.Models;

public static class ContentfulAPI
{
  private static void PrintContent(IContent content)
  {
    if (content.GetType() == typeof(Text))
    {
      var text = content as Text;
      if (text?.Value != "")
      {
        Console.WriteLine("Text: " + text?.Value);
      }
    }
  }

  public static async Task GetAllEntries()
  {
    // This client should only be created once per application.
    var httpClient = new HttpClient();

    var client = new ContentfulClient(
      httpClient,
      ContentfulSecrets.DELIVERY_API_KEY,
      ContentfulSecrets.PREVIEW_API_KEY,
      ContentfulSecrets.CONTENTFUL_SPACE_ID
    );

    var entries = await client.GetEntriesByType<ComponentText>("componentText");

    foreach (ComponentText entry in entries.Items)
    {
      Console.WriteLine("ID: " + entry.Sys?.Id);

      Console.WriteLine("INTERNAL NAME: " + entry.InternalName);

      foreach (IContent node in entry.Content?.Content!)
      {
        switch (node)
        {
          case Paragraph p:
            foreach (IContent item in p.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading1 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading2 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading3 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading4 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading5 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
          case Heading6 h:
            foreach (IContent item in h.Content)
            {
              PrintContent(item);
            }
            break;
        }
      }

      Console.WriteLine();
    }
  }
}
