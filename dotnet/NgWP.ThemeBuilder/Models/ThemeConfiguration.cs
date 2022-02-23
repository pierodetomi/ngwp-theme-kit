namespace NgWP.ThemeBuilder.Models
{
    public class ThemeConfiguration
    {
        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Author { get; set; }

        public List<string> ThemeFeatures { get; set; } = new List<string>();
        
        public List<ThemeSection> Sections { get; set; } = new List<ThemeSection>();

        public List<ThemeMenu> Menus { get; set; } = new List<ThemeMenu>();

        public List<ThemeWidgetArea> WidgetAreas { get; set; } = new List<ThemeWidgetArea>();
    }
}