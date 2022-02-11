// See https://aka.ms/new-console-template for more information

using NgWP.ThemeBuilder;
using NgWP.ThemeBuilder.Models;

var parameters = new BuildThemeParameters
{
    DistPath = args[0],
    ThemeSettingsFile = args[1]
    //DistPath = "../../../../../angular/demo-theme/dist/demo-theme",
    //ThemeSettingsFile = "../../../../../angular/demo-theme/themeconfig.json"
};

var theme = new Theme();
theme.Build(parameters);