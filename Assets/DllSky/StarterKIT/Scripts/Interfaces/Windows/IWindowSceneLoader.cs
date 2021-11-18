namespace DllSky.StarterKITv2.Interfaces.Windows
{
    public interface IWindowSceneLoader
    {
        event System.Action OnSceneLoaded;

        string SceneName { get; }
        UnityEngine.AsyncOperation LoadingProc { get; }

        void LoadScene(string scene);
        void CloseLoaderWindow();
    }
}
