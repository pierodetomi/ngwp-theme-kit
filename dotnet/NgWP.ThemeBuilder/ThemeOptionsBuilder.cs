using NgWP.ThemeBuilder.Models;
using System.Text.RegularExpressions;

namespace NgWP.ThemeBuilder
{
    public class ThemeOptionsBuilder : ThemeFileBuilderBase
    {
        private string _optionsTemplate;
        
        private string _sectionEntryTemplate;
        
        private string _settingTextEntryTemplate;
        
        private string _setSettingValueJSEntryTemplate;

        public override string FileName => "options.php";

        public ThemeOptionsBuilder(ThemeConfiguration configuration) : base(configuration) { }

        public override string BuildCode()
        {
            ReadTemplates();

            var sectionsCode = new List<string>();
            var setSettingValueJSEntryCode = new List<string>();

            configuration.Sections.ForEach(section =>
            {
                sectionsCode.Add(GetSectionEntryCode(section));

                section.Settings.ForEach(setting =>
                {
                    setSettingValueJSEntryCode.Add(GetSettingValueJSEntryCode(setting));
                });
            });

            return _optionsTemplate
                .Replace("{{theme-name}}", configuration.Name)
                .Replace("{{theme-name-slug}}", GetSlug(configuration.Name))
                .Replace("{{theme-name-func-safe}}", GetSafeId(configuration.Name, lowercase: true))
                .Replace("{{sections}}", string.Join(Environment.NewLine, sectionsCode))
                .Replace("{{set-settings-values-js-entries}}", string.Join(Environment.NewLine, setSettingValueJSEntryCode));
        }

        public override void Cleanup() { }

        private void ReadTemplates()
        {
            _optionsTemplate = ReadTemplate("options/options.php");
            _sectionEntryTemplate = ReadTemplate("options/section-entry.php");
            _settingTextEntryTemplate = ReadTemplate("options/setting-text-entry.php");
            _setSettingValueJSEntryTemplate = ReadTemplate("options/set-setting-value-js-entry.js");
        }

        private string GetSectionEntryCode(ThemeSection section)
        {
            var code = _sectionEntryTemplate.Replace("{{name}}", section.Name);

            var settingsCode = new List<string>();
            section.Settings.ForEach(setting =>
            {
                switch (setting.ControlType)
                {
                    case "text":
                        settingsCode.Add(GetSettingEntryCode(setting));
                        break;

                    default:
                        break;
                }
            });

            return code.Replace("{{settings}}", string.Join(Environment.NewLine, settingsCode));
        }

        private string GetSettingEntryCode(ThemeSetting setting)
        {
            return _settingTextEntryTemplate
                .Replace("{{name}}", setting.Label)
                .Replace("{{id}}", setting.Id);
        }

        private string GetSettingValueJSEntryCode(ThemeSetting setting)
        {
            var safeId = GetSafeId(setting.Id);

            return _setSettingValueJSEntryTemplate
                .Replace("{{id}}", setting.Id)
                .Replace("{{safe-id}}", safeId);
        }

        private string GetSafeId(string id, bool lowercase = false)
        {
            var safeId = id
                .Replace(" ", "_")
                .Replace("-", "_");

            return lowercase ? safeId.ToLower() : safeId;
        }

        private string GetSlug(string text)
        {
            return Regex
                .Replace(text, @"[^a-zA-Z0-9_]+", "-")
                .Replace("_", "-")
                .ToLower();
        }
    }
}