namespace UnityGameToolkit
{
    public interface IApp
    {
        int Initialize();
        int Start();
        void Stop();
        void Pause();
        void Resume();
        void Destroy();
    }
}
