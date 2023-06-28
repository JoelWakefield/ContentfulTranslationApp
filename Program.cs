class Program
{
  static async Task Main(string[] args)
  {
    await ContentfulAPI.GetAllEntries();

    // await AzureTranslator.Translate();
  }
}