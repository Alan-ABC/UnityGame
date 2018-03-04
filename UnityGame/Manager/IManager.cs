using UnityGameToolkit;

namespace UnityGameToolkit
{
    public interface IManager : IRecyclable
    {
        ServerStatus Status { get; }

        void Create();
        void Start();
        void Stop();
        void Destroy();
        void DestroyDirectly();
        void Update();
    }
}

