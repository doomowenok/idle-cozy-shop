using Scellecs.Morpeh;

namespace Infrastructure.EntityWorldManager
{
    public interface IWorldManager
    {
        IWorldManager CreateWorld(WorldType type);
        IWorldManager AddSystem<TSystem>() where TSystem : ISystem, new();
        void StartWorld(WorldType type);
        void StopWorld(WorldType type);
    }
}