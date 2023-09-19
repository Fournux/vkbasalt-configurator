namespace UI.Windows.About;

public static class AboutWindow
{
    public static void Show(Gtk.Window parent)
    {
        Adw.AboutWindow dialog = Adw.AboutWindow.New();
        dialog.SetTransientFor(parent);
        dialog.SetIconName(APP_ID);
        dialog.SetApplicationName("vkBasalt Configurator");
        dialog.SetApplicationIcon(APP_ID);
        dialog.SetVersion("0.1");
        // dialog.SetDebugInfo("");
        // dialog.SetComments("");
        dialog.SetDeveloperName("Fournux");
        dialog.SetLicenseType(Gtk.License.MitX11);
        dialog.SetCopyright("© Fournux 2023");
        dialog.SetWebsite("https://fournux.lu/");
        // dialog.SetIssueUrl("");
        // dialog.SetSupportUrl("");
        dialog.AddLink("GitHub Repo", "");
        dialog.SetDevelopers(new string[] { "Fournux" });
        // dialog.SetDesigners(new string[] { "" });
        // dialog.SetArtists(new string[] { "" });
        // dialog.SetTranslatorCredits("");
        // dialog.SetReleaseNotes("");
        dialog.Present();
    }
}