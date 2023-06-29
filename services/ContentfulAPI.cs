using Contentful.Core;

public static class ContentfulAPI
{
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

    await TextManager.Read(client);
  }
}
