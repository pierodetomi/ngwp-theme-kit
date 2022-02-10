using NgWP.ThemeBuilder.Interop;

namespace NgWP.ThemeBuilder.Scopes
{
    public class ConsoleHiddenScope : IDisposable
    {
        public ConsoleHiddenScope()
        {
            InteropHelper.HideConsole();
        }

        public void Dispose()
        {
            InteropHelper.ShowConsole();
        }
    }
}