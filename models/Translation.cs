public class Translation : ContentfulBase
{
  public string? Text { get; set; }
  public string? Lang { get; set; }
}

public class MSTranslation : Translation
{
  public string? To
  {
    get { return Lang; }
    set { Lang = value; }
  }
}

public class TranslationSet
{
  public List<Translation>? Translations { get; set; }
}

public class MSTranslationSet
{
  public List<MSTranslation>? Translations { get; set; }
}