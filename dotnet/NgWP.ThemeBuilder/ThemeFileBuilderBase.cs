using HtmlAgilityPack;
using Microsoft.Extensions.FileProviders;
using NgWP.ThemeBuilder.Models;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NgWP.ThemeBuilder
{
    public abstract class ThemeFileBuilderBase
    {
        private readonly EmbeddedFileProvider _embeddedProvider;

        protected readonly ThemeConfiguration configuration;

        protected string distPath;

        public abstract string FileName { get; }

        public ThemeFileBuilderBase(ThemeConfiguration configuration)
        {
            this.configuration = configuration;
            
            _embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        }

        public void Build(string distPath)
        {
            this.distPath = distPath;

            var code = BuildCode();
            Write(code);

            Cleanup();
        }

        public abstract string BuildCode();

        public abstract void Cleanup();

        protected string ReadTemplate(string subpath)
        {
            using var stream = _embeddedProvider.GetFileInfo($"Templates/{subpath}").CreateReadStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        protected string GetSourceFilePath(string fileNameRegex)
        {
            var distFiles = Directory.GetFiles(distPath);
            return distFiles.First(fileName => Regex.IsMatch(fileName, fileNameRegex));
        }

        protected string GetSourceFileName(string fileNameRegex)
        {
            var sourceFilePath = GetSourceFilePath(fileNameRegex);
            return Path.GetFileName(sourceFilePath);
        }

        protected string ReadSourceFile(string fileNameRegex)
        {
            var distFiles = Directory.GetFiles(distPath);
            var sourceFileName = distFiles.First(fileName => Regex.IsMatch(fileName, fileNameRegex));
            return File.ReadAllText(sourceFileName);
        }

        protected HtmlDocument ReadSourceHtml(string fileNameRegex)
        {
            var sourceHtml = ReadSourceFile(fileNameRegex);
            
            var html = new HtmlDocument();
            html.LoadHtml(sourceHtml);

            return html;
        }

        private void Write(string code)
        {
            var filePath = Path.Combine(distPath, FileName);
            File.WriteAllText(filePath, code);
        }
    }
}
