namespace Infrastructure.SaveLoad
{
    public interface ILoadable
    {
        void Initialize();
        void LoadSave();
    }
}