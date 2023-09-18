#pragma warning disable CA1707

namespace Constants;

public static class ApplicationIdentity
{
    /// <summary>The application ID that is used to identify your application,
    /// see https://developer.gnome.org/documentation/tutorials/application-id.html.
    /// This should be automatically replaced when the application is created.
    /// </summary>
    public const string APP_ID = "lu.fournux.vkbasalt.configurator";

    /// <summary>
    /// A shorter name for the application.
    /// This is case sensitive and should not contain spaces.
    /// This should be automatically replaced whe   n the application is created.
    /// </summary>
    public const string APP_SHORT_NAME = "vkbasalt_configurator";

    /// <summary>
    /// The display name of the application.
    /// This should be automatically replaced when the application is created.
    /// </summary>
    public const string APP_DISPLAY_NAME = "vkbasalt-configurator";
}