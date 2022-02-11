using Microsoft.Extensions.FileProviders;
using NgWP.ThemeBuilder.Models;
using System.Reflection;

namespace NgWP.ThemeBuilder
{
    public class ThemeFunctions
    {
        private ThemeConfiguration _configuration;

        private string _functionsTemplate;

        private string _addThemeFeatureEntryTemplate;

        private string _addSectionEntryTemplate;

        private string _addSettingEntryTemplate;

        private string _addImgSettingEntryTemplate;

        private string _addTxtSettingEntryTemplate;

        public ThemeFunctions(ThemeConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Build(string distPath)
        {
            ReadTemplates();

            var featuresCode = new List<string>();
            var sectionsCode = new List<string>();
            var settingsCode = new List<string>();

            _configuration.ThemeFeatures.ForEach(feature =>
            {
                sectionsCode.Add(GetThemeFeatureEntryCode(feature));
            });

            _configuration.Sections.ForEach(section =>
            {
                sectionsCode.Add(GetThemeSectionEntryCode(section));

                section.Settings.ForEach(setting =>
                {
                    settingsCode.Add(GetSectionSettingEntryCode(setting, section));
                });
            });

            var functionsCode = _functionsTemplate
                .Replace("{{theme-support-entries}}", string.Join(Environment.NewLine, featuresCode))
                .Replace("{{sections-code}}", string.Join(Environment.NewLine, sectionsCode))
                .Replace("{{settings-code}}", string.Join(Environment.NewLine, settingsCode));

            var functionsFilePath = Path.Combine(distPath, "functions.php");
            File.WriteAllText(functionsFilePath, functionsCode);
        }

        private void ReadTemplates()
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

            _functionsTemplate = ReadTemplate(embeddedProvider, "functions.php");
            _addThemeFeatureEntryTemplate = ReadTemplate(embeddedProvider, "add-theme-feature-entry.php");
            _addSectionEntryTemplate = ReadTemplate(embeddedProvider, "add-section-entry.php");
            _addSettingEntryTemplate = ReadTemplate(embeddedProvider, "add-setting-entry.php");
            _addImgSettingEntryTemplate = ReadTemplate(embeddedProvider, "add-img-setting-entry.php");
            _addTxtSettingEntryTemplate = ReadTemplate(embeddedProvider, "add-txt-setting-entry.php");
        }

        private string ReadTemplate(EmbeddedFileProvider embeddedProvider, string name)
        {
            using var stream = embeddedProvider.GetFileInfo($"Templates/functions/{name}").CreateReadStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
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