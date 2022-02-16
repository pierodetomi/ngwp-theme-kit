namespace NgWP.ThemeBuilder.Models
{
    public class ThemeMenu
    {
        public string Location { get; set; }

        public string Description { get; set; }

        public string InitialName { get; set; }

        public List<string> InitialEntries { get; set; } = new List<string>();

        public bool OnlyRegisterLocation { get; set; } = true;
    }
}