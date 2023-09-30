using GetText;

namespace UI.Localization;

public static class I18n
{
    private static readonly ICatalog _catalog = new Catalog("strings", "./locales");

    public static string GetString(string key)
    {
        return _catalog.GetString(key);
    }
}