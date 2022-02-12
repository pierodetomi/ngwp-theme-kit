using NgWP.ThemeBuilder.Models;

namespace NgWP.ThemeBuilder
{
    public class ThemeStyleBuilder : ThemeFileBuilderBase
    {
        private const string SourceFileNameRegex = @".*styles.*\.css";

        private string _styleTemplate;

        public override string FileName => "style.css";

        public ThemeStyleBuilder(ThemeConfiguration configuration) : base(configuration) { }

        public override string BuildCode()
        {
            ReadTemplates();

            var styleCode = ReadSourceFile(SourceFileNameRegex);

            return _styleTemplate
                .Replace("{{name}}", configuration.Name)
                .Replace("{{description}}", configuration.Description)
                .Replace("{{author}}", configuration.Author)
                .Replace("{{year}}", DateTime.Today.Year.ToString())
                .Replace("{{style-code}}", styleCode);
        }

        public override void Cleanup()
        {
            var sourceFilePath = GetSourceFilePath(SourceFileNameRegex);
            File.Delete(sourceFilePath);
        }

        private void ReadTemplates()
        {
            _styleTemplate = ReadTemplate("style/style.css");
        }
    }
}