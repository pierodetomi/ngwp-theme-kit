using Newtonsoft.Json;
using NgWP.ThemeBuilder.Models;
using System.Reflection;

namespace NgWP.ThemeBuilder
{
    public class Theme
    {
        private ThemeConfiguration _configuration;

        private string _executionPath;
        
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

            var index = new ThemeIndexBuilder(_configuration);
            index.Build(parameters.DistPath);

            var style = new ThemeStyleBuilder(_configuration);
            style.Build(parameters.DistPath);

            var functions = new ThemeFunctionsBuilder(_configuration);
            functions.Build(parameters.DistPath);

            var options = new ThemeOptionsBuilder(_configuration);
            options.Build(parameters.DistPath);

            CopyStaticFiles(parameters);
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
    }
}
