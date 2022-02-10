// See https://aka.ms/new-console-template for more information

using NgWP.ThemeBuilder;
using NgWP.ThemeBuilder.Models;

var parameters = new BuildThemeParameters
{
    DistPath = args[0] // "../../../../../ngwp-theme/dist/ngwp-theme"
};

var theme = new Theme();
theme.Build(parameters);