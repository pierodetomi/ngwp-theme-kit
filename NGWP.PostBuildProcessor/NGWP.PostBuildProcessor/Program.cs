// See https://aka.ms/new-console-template for more information
// Console.WriteLine(args);

using HtmlAgilityPack;
using NGWP.PostBuildProcessor.Interop;
using NGWP.PostBuildProcessor.WordPress;

InteropHelper.HideConsole();

Console.WriteLine();
Console.Write("Template name: ");
var templateName = Console.ReadLine();
templateName = String.IsNullOrWhiteSpace(templateName) ? "NgWP Theme" : templateName;
Console.WriteLine();

var executionPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetFiles()[0].Name);
Console.WriteLine($"Executable directory is: ${executionPath}");
var distPath = args[0];

if (!Directory.Exists(distPath))
{
    Console.WriteLine("The path provided as \"dist\" path does not exits. Please check that it's correct and retry.");
    Console.WriteLine();
    return;
}

var indexFileName = Path.Combine(distPath, "index.html");
var stylesFileName = Path.Combine(distPath, "styles.css");
var wpIndexFileName = Path.Combine(distPath, "index.php");
var wpStyleFileName = Path.Combine(distPath, "style.css");
var themeScreenshotResourceFileName = Path.Combine(executionPath, "Resources/screenshot.png");
var wpThemeScreenshotFileName = Path.Combine(distPath, "screenshot.png");

var indexHtml = File.ReadAllText(indexFileName);

var html = new HtmlDocument();
html.LoadHtml(indexHtml);

var head = html.DocumentNode.ChildNodes.FindFirst("head");
var body = html.DocumentNode.ChildNodes.FindFirst("body");

// Add WP head PHP directive
var wpHeadNode = HtmlNode.CreateNode("<?php wp_head(); ?>");
head.AppendChild(wpHeadNode);

// Remove stylesheet reference (will be automatically applied by WordPress)
var stylesLinkNode = head.ChildNodes.First(_ => _.Name == "link" && _.GetAttributeValue("href", string.Empty) == "styles.css");
stylesLinkNode.Remove();
// stylesLinkNode.SetAttributeValue("href", "style.css");

// Add base url to (existing) scripts
var scriptNodes = body.ChildNodes.Where(_ => _.Name == "script");

foreach (var scriptNode in scriptNodes)
    scriptNode.SetAttributeValue("src", $"{PageFragments.ScriptBaseUrl}{scriptNode.GetAttributeValue("src", string.Empty)}");

//// Add WP variables script
//var wpVariablesScriptNode = HtmlNode.CreateNode(PageFragments.WPVariables);
//var firstScript = body.ChildNodes.FindFirst("script");
//body.ChildNodes.Insert(body.ChildNodes.IndexOf(firstScript), wpVariablesScriptNode);

// body.AppendChild(wpVariablesScriptNode);

// Add WP footer PHP directive
var wpFooterNode = HtmlNode.CreateNode("<?php wp_footer(); ?>");
body.AppendChild(wpFooterNode);

var finalHtml = html.DocumentNode.WriteTo();

// Add PHP page start
finalHtml = finalHtml.Insert(0, PageFragments.PageStart);

// Add WP body style
finalHtml = finalHtml.Replace("<body ", "<body <?php body_class(); ?> ");

File.WriteAllText(wpIndexFileName, finalHtml);
File.Move(stylesFileName, wpStyleFileName);

var style = File.ReadAllText(wpStyleFileName);
style = style.Insert(0, PageFragments.StyleStart);
style = style.Replace("{{templateName}}", templateName);
File.WriteAllText(wpStyleFileName, style);
File.Copy(themeScreenshotResourceFileName, wpThemeScreenshotFileName);

// Cleanup source files
File.Delete(indexFileName);
File.Delete(stylesFileName);