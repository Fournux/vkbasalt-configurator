namespace UI.Windows.About;

public static class AboutWindow
{
    public static void Show(Gtk.Window parent)
    {
        Values values = Values.Instance();

        Adw.AboutWindow dialog = Adw.AboutWindow.New();
        dialog.SetTransientFor(parent);
        dialog.SetIconName(values.APP_ID);
        dialog.SetApplicationName("vkBasalt Configurator");
        dialog.SetApplicationIcon(values.APP_ID!);
        dialog.SetVersion(values.APP_VERSION!);
        dialog.SetDeveloperName("Fournux");
        dialog.SetLicenseType(Gtk.License.MitX11);
        dialog.SetCopyright("© Fournux " + DateTime.Now.Year);
        dialog.SetWebsite("https://fournux.lu/");
        dialog.SetIssueUrl("https://github.com/Fournux/vkbasalt-configurator/issues");
        dialog.AddLink("GitHub Repo", "https://github.com/Fournux/vkbasalt-configurator");
        dialog.SetDevelopers(new string[] { "Fournux" });
        dialog.Present();
    }
}