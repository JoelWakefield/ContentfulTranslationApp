public class Translation
{
  public string? Text { get; set; }
  public string? Lang { get; set; }
}

public class TranslationSet
{
  public List<Translation>? Translations { get; set; }
}