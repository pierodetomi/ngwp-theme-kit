namespace NgWP.ThemeBuilder.Models
{
    public class ThemeSection
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<ThemeSetting> Settings { get; set; } = new List<ThemeSetting>();
    }
}