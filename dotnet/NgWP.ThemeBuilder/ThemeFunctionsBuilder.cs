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

        private string _menuLocationsRegistrationTemplate;

        private string _menuLocationEntryTemplate;

        private string _menuCreationTemplate;

        private string _widgetAreaRegistrationTemplate;

        public override string FileName => "functions.php";

        public ThemeFunctionsBuilder(ThemeConfiguration configuration) : base(configuration) { }

        public override string BuildCode()
        {
            ReadTemplates();

            var featuresCode = new List<string>();
            var sectionsCode = new List<string>();
            var settingsCode = new List<string>();
            var menuLocationRegistrationsCode = string.Empty;
            var menuLocationsCode = new List<string>();
            var menusCreationCode = new List<string>();
            var widgetAreaRegistrationsCode = new List<string>();

            // Add theme support for menus if they're present in configuration file
            if (!configuration.ThemeFeatures.Contains("menus") && configuration.Menus.Any())
                configuration.ThemeFeatures.Add("menus");

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

            if (configuration.Menus.Any())
            {
                configuration.Menus.ForEach(menu =>
                {
                    menuLocationsCode.Add(GetMenuLocationEntryCode(menu));

                    if (CanCreateMenuInstance(menu))
                        menusCreationCode.Add(GetMenuCreationEntryCode(menu));
                });

                menuLocationRegistrationsCode = GetMenuLocationsCode(menuLocationsCode);
            }

            if (configuration.WidgetAreas.Any())
                configuration.WidgetAreas.ForEach(widgetArea =>
                {
                    widgetAreaRegistrationsCode.Add(GetWidgetAreaRegistrationCode(widgetArea));
                });

            return _functionsTemplate
                .Replace("{{theme-support-entries}}", string.Join(Environment.NewLine, featuresCode))
                .Replace("{{menus-registration-code}}", menuLocationRegistrationsCode)
                .Replace("{{menus-creation-code}}", string.Join(Environment.NewLine, menusCreationCode))
                .Replace("{{widget-areas-registration-code}}", string.Join(Environment.NewLine, widgetAreaRegistrationsCode))
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
            _menuLocationsRegistrationTemplate = ReadTemplate("functions/menu-location-registration.php");
            _menuLocationEntryTemplate = ReadTemplate("functions/menu-location-entry.php");
            _menuCreationTemplate = ReadTemplate("functions/menu-creation.php");
            _widgetAreaRegistrationTemplate = ReadTemplate("functions/widget-area-registration.php");
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

        private string GetMenuLocationEntryCode(ThemeMenu menu)
        {
            return _menuLocationEntryTemplate
                .Replace("{{location}}", menu.Location)
                .Replace("{{description}}", menu.Description);
        }

        private string GetMenuLocationsCode(List<string> menuEntriesCode)
        {
            return _menuLocationsRegistrationTemplate
                .Replace("{{theme-menus-code}}", string.Join(",", menuEntriesCode));
        }

        private bool CanCreateMenuInstance(ThemeMenu menu)
        {
            return (
                !menu.OnlyRegisterLocation &&
                !string.IsNullOrEmpty(menu.InitialName) &&
                menu.InitialEntries != null &&
                menu.InitialEntries.Any()
            );
        }

        private string GetMenuCreationEntryCode(ThemeMenu menu)
        {
            return _menuCreationTemplate
                .Replace("{{name}}", menu.InitialName)
                .Replace("{{location}}", menu.Location)
                .Replace("{{entries}}", string.Join(",", menu.InitialEntries.Select(_ => $"\"{_}\"")));
        }

        private string GetWidgetAreaRegistrationCode(ThemeWidgetArea widgetArea)
        {
            return _widgetAreaRegistrationTemplate
                .Replace("{{id}}", widgetArea.Id)
                .Replace("{{name}}", widgetArea.Name)
                .Replace("{{description}}", widgetArea.Description);
        }
    }
}