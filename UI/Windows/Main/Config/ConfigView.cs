using Core;
using UI.Components;
using UI.Extensions;
using static UI.Localization.CatalogManager;

namespace UI.Windows.Main.Config;

public class ConfigView : Gtk.ListBox
{
    private readonly KeySelectorRow keySelectorRow;
    private readonly SwitchRow switchRow;
    private readonly CasSettings casSettings;
    private readonly DlsSettings dlsSettings;
    private readonly FxaaSettings fxaaSettings;
    private readonly SmaaSettings smaaSettings;

    public ConfigView()
    {
        SetMarginTop(10);
        SetHexpand(true);
        SetSelectionMode(Gtk.SelectionMode.None);
        SetValign(Gtk.Align.Start);
        AddCssClass("boxed-list");

        keySelectorRow = new KeySelectorRow(GetString("Toggle key"), GetString("Press any key"));
        switchRow = new SwitchRow(GetString("Enable on start"));
        casSettings = new CasSettings();
        dlsSettings = new DlsSettings();
        fxaaSettings = new FxaaSettings();
        smaaSettings = new SmaaSettings();


        this.Append(switchRow, keySelectorRow, casSettings, dlsSettings, fxaaSettings, smaaSettings);
    }

    public void LoadConfigFile(ConfigFile configFile)
    {
        bool enableOnLaunch = configFile.Get<bool>(ConfigKey.EnableOnLaunch);
        string toggleKey = configFile.Get<string>(ConfigKey.ToggleKey);

        string effects = configFile.Get<string>(ConfigKey.Effects);

        bool casEnabled = effects.Contains("cas");
        double casSharpness = configFile.Get<double>(ConfigKey.CasSharpness);

        bool dlsEnabled = effects.Contains("dls");
        double dlsSharpness = configFile.Get<double>(ConfigKey.DlsSharpness);
        double dlsDenoise = configFile.Get<double>(ConfigKey.DlsDenoise);

        bool fxaaEnabled = effects.Contains("fxaa");
        double fxaaQualitySubpix = configFile.Get<double>(ConfigKey.FxaaQualitySubpix);
        double fxaaQualityEdgeThreshold = configFile.Get<double>(ConfigKey.FxaaQualityEdgeThreshold);
        double fxaaQualityEdgeThresholdMin = configFile.Get<double>(ConfigKey.FxaaQualityEdgeThresholdMin);

        bool smaaEnabled = effects.Contains("smaa");
        string smaaEdgeDetection = configFile.Get<string>(ConfigKey.SmaaEdgeDetection);
        double smaaThreshold = configFile.Get<double>(ConfigKey.SmaaThreshold);
        double smaaMaxSearchSteps = configFile.Get<double>(ConfigKey.SmaaMaxSearchSteps);
        double smaaMaxSearchStepsDiag = configFile.Get<double>(ConfigKey.SmaaMaxSearchStepsDiag);
        double smaaCornerRounding = configFile.Get<double>(ConfigKey.SmaaCornerRounding);

        switchRow.Active = enableOnLaunch;
        keySelectorRow.Key = toggleKey;

        casSettings.Enabled = casEnabled;
        casSettings.Sharpness = casSharpness;

        dlsSettings.Enabled = dlsEnabled;
        dlsSettings.Sharpness = dlsSharpness;
        dlsSettings.Denoise = dlsDenoise;

        fxaaSettings.Enabled = fxaaEnabled;
        fxaaSettings.Subpixel = fxaaQualitySubpix;
        fxaaSettings.Edge = fxaaQualityEdgeThreshold;
        fxaaSettings.EdgeMin = fxaaQualityEdgeThresholdMin;

        smaaSettings.Enabled = smaaEnabled;
        smaaSettings.Edge = smaaThreshold;
        smaaSettings.Steps = smaaMaxSearchSteps;
        smaaSettings.DiagSteps = smaaMaxSearchStepsDiag;
        smaaSettings.Corner = smaaCornerRounding;
        smaaSettings.EdgeDetection = smaaEdgeDetection == "color" ? SmaaEdgeDetection.Color : SmaaEdgeDetection.Luma;
    }

    public void UpdateConfigFile(ConfigFile configFile)
    {
        configFile.Set(ConfigKey.EnableOnLaunch, switchRow.Active);
        configFile.Set(ConfigKey.ToggleKey, keySelectorRow.Key);
        configFile.Set(ConfigKey.CasSharpness, casSettings.Sharpness);
        configFile.Set(ConfigKey.DlsSharpness, dlsSettings.Sharpness);
        configFile.Set(ConfigKey.DlsDenoise, dlsSettings.Denoise);
        configFile.Set(ConfigKey.FxaaQualitySubpix, fxaaSettings.Subpixel);
        configFile.Set(ConfigKey.FxaaQualityEdgeThreshold, fxaaSettings.Edge);
        configFile.Set(ConfigKey.FxaaQualityEdgeThresholdMin, fxaaSettings.EdgeMin);
        configFile.Set(ConfigKey.SmaaEdgeDetection, smaaSettings.EdgeDetection.ToString().ToLowerInvariant());
        configFile.Set(ConfigKey.SmaaThreshold, smaaSettings.Edge);
        configFile.Set(ConfigKey.SmaaMaxSearchSteps, smaaSettings.Steps);
        configFile.Set(ConfigKey.SmaaMaxSearchStepsDiag, smaaSettings.DiagSteps);
        configFile.Set(ConfigKey.SmaaCornerRounding, smaaSettings.Corner);

        List<string> effects = new();
        if (casSettings.Enabled)
        {
            effects.Add("cas");
        }

        if (dlsSettings.Enabled)
        {
            effects.Add("dls");
        }

        if (fxaaSettings.Enabled)
        {
            effects.Add("fxaa");
        }

        if (smaaSettings.Enabled)
        {
            effects.Add("smaa");
        }

        configFile.Set(ConfigKey.Effects, string.Join(':', effects));
    }
}