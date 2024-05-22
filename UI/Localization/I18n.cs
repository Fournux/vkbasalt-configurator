using GetText;

namespace UI.Localization;

public static class I18n
{
    private static readonly Catalog _catalog = new("strings", "./locales");

    public static string GetString(string key)
    {
        return _catalog.GetString(key);
    }
}