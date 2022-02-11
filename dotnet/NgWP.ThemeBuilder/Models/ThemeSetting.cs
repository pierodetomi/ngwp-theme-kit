namespace NgWP.ThemeBuilder.Models
{
    public class ThemeSetting
    {
        public string Id { get; set; }
        
        public string Label { get; set; }
        
        public string Description { get; set; }

        public object DefaultValue { get; set; }

        public string ControlType { get; set; } = "text";
    }
}