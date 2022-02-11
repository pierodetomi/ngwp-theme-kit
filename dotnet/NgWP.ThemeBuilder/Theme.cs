using HtmlAgilityPack;
using Newtonsoft.Json;
using NgWP.ThemeBuilder.Models;
using NgWP.ThemeBuilder.Scopes;
using System.Reflection;

namespace NgWP.ThemeBuilder
{
    public class Theme
    {
        private ThemeConfiguration _configuration;

        private string _executionPath;
        
        private string _ngIndexFilePath;
        
        private string _ngStylesFilePath;
        
        private string _ngStylesFileName;
        
        private string _wpIndexFileName;
        
        public string ExecutionPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_executionPath))
                    return _executionPath;

                var executableFileName = Assembly.GetExecutingAssembly().GetFiles()[0].Name;
                _executionPath = Path.GetDirectoryName(executableFileName)!;
                
                return _executionPath;
            }
        }

        public void Build(BuildThemeParameters parameters)
        {
            // using var scope = new ConsoleHiddenScope();

            if (!CheckInputParameters(parameters))
                return;

            ReadSettings(parameters.ThemeSettingsFile);
            InitializeFilePaths(parameters);

            ProcessIndexPage(parameters);
            ProcessStyles(parameters);

            var functions = new ThemeFunctions(_configuration);
            functions.Build(parameters.DistPath);

            CopyStaticFiles(parameters);
            CleanupSource();
        }

        private bool CheckInputParameters(BuildThemeParameters parameters)
        {
            if (parameters.DistPath == null)
            {
                Console.WriteLine($"Dist path parameter not provided!");
                Console.WriteLine();

                return false;
            }
            else if (!Directory.Exists(parameters.DistPath))
            {
                Console.WriteLine($"Dist path \"{parameters.DistPath}\" does not exits. Please check that it's correct and retry.");
                Console.WriteLine();
                
                return false;
            }

            if (parameters.ThemeSettingsFile == null)
            {
                Console.WriteLine($"Theme settings file parameter not provided!");
                Console.WriteLine();

                return false;
            }
            else if (!File.Exists(parameters.ThemeSettingsFile))
            {
                Console.WriteLine($"Theme settings file \"{parameters.ThemeSettingsFile}\" does not exits. Please check that it's correct and retry.");
                Console.WriteLine();

                return false;
            }

            return true;
        }

        private void ReadSettings(string themeSettingsFile)
        {
            string settingsJson = File.ReadAllText(themeSettingsFile);
            _configuration = JsonConvert.DeserializeObject<ThemeConfiguration>(settingsJson);
        }

        private void InitializeFilePaths(BuildThemeParameters parameters)
        {
            var distFiles = Directory.GetFiles(parameters.DistPath);

            _ngIndexFilePath = distFiles.First(_ => _.EndsWith("index.html"));

            _ngStylesFilePath = distFiles.First(_ => _.Contains("styles") && _.EndsWith(".css"));
            _ngStylesFileName = Path.GetFileName(_ngStylesFilePath);

            _wpIndexFileName = Path.Combine(parameters.DistPath!, "index.php");
        }

        private void ProcessIndexPage(BuildThemeParameters parameters)
        {
            var indexHtml = File.ReadAllText(_ngIndexFilePath);

            var html = new HtmlDocument();
            html.LoadHtml(indexHtml);

            var head = html.DocumentNode.ChildNodes.FindFirst("head");
            var body = html.DocumentNode.ChildNodes.FindFirst("body");

            // Add WP head PHP directive
            var wpHeadNode = HtmlNode.CreateNode("<?php wp_head(); ?>");
            head.AppendChild(wpHeadNode);

            // Remove stylesheet reference (will be automatically applied by WordPress)
            var stylesLinkNode = head.ChildNodes.First(_ => _.Name == "link" && _.GetAttributeValue("href", string.Empty) == _ngStylesFileName);
            stylesLinkNode.Remove();
            
            // Add base url to (existing) scripts
            var scriptNodes = body.ChildNodes.Where(_ => _.Name == "script");

            foreach (var scriptNode in scriptNodes)
                scriptNode.SetAttributeValue("src", $"{Constants.PageFragments.ScriptBaseUrl}{scriptNode.GetAttributeValue("src", string.Empty)}");

            // Add WP variables script
            var wpVariablesScriptNode = HtmlNode.CreateNode(Constants.PageFragments.WPVariables);
            var firstScript = body.ChildNodes.FindFirst("script");
            body.ChildNodes.Insert(body.ChildNodes.IndexOf(firstScript), wpVariablesScriptNode);

            // Add WP footer PHP directive
            var wpFooterNode = HtmlNode.CreateNode("<?php wp_footer(); ?>");
            body.AppendChild(wpFooterNode);

            var finalHtml = html.DocumentNode.WriteTo();

            // Add PHP page start
            finalHtml = finalHtml.Insert(0, Constants.PageFragments.PageStart);

            // Add WP body style
            finalHtml = finalHtml.Replace("<body ", "<body <?php body_class(); ?> ");

            File.WriteAllText(_wpIndexFileName, finalHtml);
        }

        private void ProcessStyles(BuildThemeParameters parameters)
        {
            var wpStyleFileName = Path.Combine(parameters.DistPath!, "style.css");
            File.Move(_ngStylesFilePath, wpStyleFileName);

            var style = File.ReadAllText(wpStyleFileName);
            
            style = style
                .Insert(0, Constants.PageFragments.StyleStart)
                .Replace("{{name}}", _configuration.Name)
                .Replace("{{description}}", _configuration.Description)
                .Replace("{{author}}", _configuration.Author)
                .Replace("{{year}}", DateTime.Today.Year.ToString());

            File.WriteAllText(wpStyleFileName, style);
        }

        private void CopyStaticFiles(BuildThemeParameters parameters)
        {
            var staticFilesPath = Path.Combine(ExecutionPath, "Resources");
            CopyFilesRecursively(staticFilesPath, parameters.DistPath);
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void CleanupSource()
        {
            // Cleanup source files
            File.Delete(_ngIndexFilePath);
            File.Delete(_ngStylesFilePath);
        }
    }
}
