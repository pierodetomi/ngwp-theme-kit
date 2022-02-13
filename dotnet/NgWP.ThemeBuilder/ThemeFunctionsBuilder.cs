using NgWP.ThemeBuilder.Models;

namespace NgWP.ThemeBuilder
{
    public class ThemeFunctionsBuilder : ThemeFileBuilderBase
    {
        private string _functionsTemplate;

        private string _addThemeFeatureEntryTemplate;

        private string _addSectionEntryTemplate;

        private string _addSettingEntryTemplate;

        private string _addImgSettingEntryTemplate;

        private string _addTxtSettingEntryTemplate;

        public override string FileName => "functions.php";

        public ThemeFunctionsBuilder(ThemeConfiguration configuration) : base(configuration) { }

        public override string BuildCode()
        {
            ReadTemplates();

            var featuresCode = new List<string>();
            var sectionsCode = new List<string>();
            var settingsCode = new List<string>();

            configuration.ThemeFeatures.ForEach(feature =>
            {
                featuresCode.Add(GetThemeFeatureEntryCode(feature));
            });

            configuration.Sections.ForEach(section =>
            {
                sectionsCode.Add(GetThemeSectionEntryCode(section));

                section.Settings.ForEach(setting =>
                {
                    settingsCode.Add(GetSectionSettingEntryCode(setting, section));
                });
            });

            return _functionsTemplate
                .Replace("{{theme-support-entries}}", string.Join(Environment.NewLine, featuresCode))
                .Replace("{{sections-code}}", string.Join(Environment.NewLine, sectionsCode))
                .Replace("{{settings-code}}", string.Join(Environment.NewLine, settingsCode));
        }

        public override void Cleanup() { }

        private void ReadTemplates()
        {
            _functionsTemplate = ReadTemplate("functions/functions.php");
            _addThemeFeatureEntryTemplate = ReadTemplate("functions/add-theme-feature-entry.php");
            _addSectionEntryTemplate = ReadTemplate("functions/add-section-entry.php");
            _addSettingEntryTemplate = ReadTemplate("functions/add-setting-entry.php");
            _addImgSettingEntryTemplate = ReadTemplate("functions/add-img-setting-entry.php");
            _addTxtSettingEntryTemplate = ReadTemplate("functions/add-txt-setting-entry.php");
        }

        private string GetThemeFeatureEntryCode(string feature)
        {
            return _addThemeFeatureEntryTemplate.Replace("{{feature}}", feature);
        }

        private string GetThemeSectionEntryCode(ThemeSection section)
        {
            return _addSectionEntryTemplate
                .Replace("{{id}}", section.Id)
                .Replace("{{name}}", section.Name)
                .Replace("{{description}}", section.Description);
        }

        private string GetSectionSettingEntryCode(ThemeSetting setting, ThemeSection section)
        {
            var defaultValue = setting.DefaultValue == null ? "null" : Convert.ToString(setting.DefaultValue);
            
            return setting.ControlType switch
            {
                "image" => _addImgSettingEntryTemplate
                    .Replace("{{id}}", setting.Id)
                    .Replace("{{section-id}}", section.Id)
                    .Replace("{{label}}", setting.Label)
                    .Replace("{{description}}", setting.Description),

                "text" => _addTxtSettingEntryTemplate
                    .Replace("{{id}}", setting.Id)
                    .Replace("{{section-id}}", section.Id)
                    .Replace("{{label}}", setting.Label)
                    .Replace("{{description}}", setting.Description)
                    .Replace("{{default-value}}", defaultValue), // TODO: default value handling

                _ => _addSettingEntryTemplate
                    .Replace("{{id}}", setting.Id)
                    .Replace("{{section-id}}", section.Id)
                    .Replace("{{label}}", setting.Label)
                    .Replace("{{description}}", setting.Description)
                    .Replace("{{default-value}}", defaultValue) // TODO: default value handling
                    .Replace("{{control-type}}", setting.ControlType),
            };
        }
    }
}