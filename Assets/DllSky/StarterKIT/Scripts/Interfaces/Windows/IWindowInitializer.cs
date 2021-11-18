namespace DllSky.StarterKITv2.Interfaces.Windows
{
    public interface IWindowInitializer
    {
        event System.Action OnInitialize;

        bool IsInit { get; }
    }
}
