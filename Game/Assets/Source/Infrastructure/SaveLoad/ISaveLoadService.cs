namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadService
    {
        void RegisterManually<TData>(TData data) where TData : ILoadable;
        void LoadSave();
        void RegisterAutomatically();
    }
}