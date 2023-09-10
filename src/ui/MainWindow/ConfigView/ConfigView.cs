using core;
using GLib;

namespace ui;

public class ConfigView : Gtk.Box
{
#pragma warning disable 0649
#pragma warning restore 0649

    private readonly KeySelectorRow keySelectorRow;
    private readonly SwitchRow switchRow;
    private readonly CasSettings casSettings;
    private readonly DlsSettings dlsSettings;
    private readonly FxaaSettings fxaaSettings;
    private readonly SmaaSettings smaaSettings;

    public ConfigView()
    {
        this.SetOrientation(Gtk.Orientation.Vertical);
        this.SetMarginTop(10);
        this.SetSpacing(10);

        this.keySelectorRow = new KeySelectorRow("Toggle key");
        this.switchRow = new SwitchRow("Enable on start");
        this.casSettings = new CasSettings();
        this.dlsSettings = new DlsSettings();
        this.fxaaSettings = new FxaaSettings();
        this.smaaSettings = new SmaaSettings();

        this.Append(switchRow);
        this.Append(keySelectorRow);
        this.Append(casSettings);
        this.Append(dlsSettings);
        this.Append(fxaaSettings);
        this.Append(smaaSettings);
    }

    public void LoadConfigFile(ConfigFile configFile)
    {
        var enableOnLaunch = configFile.Get<bool>(ConfigKey.EnableOnLaunch);
        var toggleKey = configFile.Get<string>(ConfigKey.ToggleKey);

        var effects = configFile.Get<string>(ConfigKey.Effects);

        var casEnabled = effects.Contains("cas");
        var casSharpness = configFile.Get<double>(ConfigKey.CasSharpness);

        var dlsEnabled = effects.Contains("dls");
        var dlsSharpness = configFile.Get<double>(ConfigKey.DlsSharpness);
        var dlsDenoise = configFile.Get<double>(ConfigKey.DlsDenoise);

        var fxaaEnabled = effects.Contains("fxaa");
        var fxaaQualitySubpix = configFile.Get<double>(ConfigKey.FxaaQualitySubpix);
        var fxaaQualityEdgeThreshold = configFile.Get<double>(ConfigKey.FxaaQualityEdgeThreshold);
        var fxaaQualityEdgeThresholdMin = configFile.Get<double>(ConfigKey.FxaaQualityEdgeThresholdMin);

        var smaaEnabled = effects.Contains("smaa");
        var smaaEdgeDetection = configFile.Get<string>(ConfigKey.SmaaEdgeDetection);
        var smaaThreshold = configFile.Get<double>(ConfigKey.SmaaThreshold);
        var smaaMaxSearchSteps = configFile.Get<double>(ConfigKey.SmaaMaxSearchSteps);
        var smaaMaxSearchStepsDiag = configFile.Get<double>(ConfigKey.SmaaMaxSearchStepsDiag);
        var smaaCornerRounding = configFile.Get<double>(ConfigKey.SmaaCornerRounding);

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

        var effects = new List<string>();
        if (casSettings.Enabled) effects.Add("cas");
        if (dlsSettings.Enabled) effects.Add("dls");
        if (fxaaSettings.Enabled) effects.Add("fxaa");
        if (smaaSettings.Enabled) effects.Add("smaa");

        configFile.Set(ConfigKey.Effects, string.Join(':', effects));
    }
}