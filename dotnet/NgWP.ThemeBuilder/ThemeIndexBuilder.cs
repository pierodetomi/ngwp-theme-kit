using NgWP.ThemeBuilder.Models;

namespace NgWP.ThemeBuilder
{
    public class ThemeIndexBuilder : ThemeFileBuilderBase
    {
        private const string SourceFileNameRegex = @"index\.html";

        private string _indexTemplate;

        private string _scriptSrcTemplate;

        public override string FileName => "index.php";

        public ThemeIndexBuilder(ThemeConfiguration configuration) : base(configuration) { }

        public override string BuildCode()
        {
            ReadTemplates();

            var html = ReadSourceHtml(SourceFileNameRegex);
            var head = html.DocumentNode.ChildNodes.FindFirst("head");
            var body = html.DocumentNode.ChildNodes.FindFirst("body");

            // Remove stylesheet reference (will be automatically applied by WordPress)
            var ngStylesFileName = GetSourceFileName(@".*styles.*\.css");
            var stylesLinkNode = head.ChildNodes.First(_ => _.Name == "link" && _.GetAttributeValue("href", string.Empty) == ngStylesFileName);
            stylesLinkNode.Remove();
            var headCode = head.InnerHtml;
            
            // Get scripts code
            var scripts = body.ChildNodes.Where(_ => _.Name == "script").ToList();
            var updatedScripts = new List<string>();
            scripts.ForEach(script =>
            {
                // Add base url
                var currentScriptSrc = script.GetAttributeValue("src", string.Empty);
                var wpScriptSrc = GetScriptSrcEntryCode(currentScriptSrc);

                script.SetAttributeValue("src", wpScriptSrc);
                updatedScripts.Add(script.WriteTo());
            });
            var scriptsCode = string.Join(Environment.NewLine, updatedScripts);

            // Get body content (without scripts)
            scripts.ForEach(_ => _.Remove());
            var bodyCode = body.InnerHtml;

            return _indexTemplate
                .Replace("{{head-code}}", headCode)
                .Replace("{{body-code}}", bodyCode)
                .Replace("{{scripts-code}}", string.Join(Environment.NewLine, updatedScripts));
        }

        public override void Cleanup()
        {
            var sourceFilePath = GetSourceFilePath(SourceFileNameRegex);
            File.Delete(sourceFilePath);
        }

        private void ReadTemplates()
        {
            _indexTemplate = ReadTemplate("index/index.php");
            _scriptSrcTemplate = ReadTemplate("index/script-src-entry.php");
        }

        private string GetScriptSrcEntryCode(string currentScriptSrc)
        {
            return _scriptSrcTemplate.Replace("{{originalSrc}}", currentScriptSrc);
        }
    }
}