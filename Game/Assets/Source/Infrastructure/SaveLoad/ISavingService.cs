namespace Infrastructure.SaveLoad
{
    public interface ISavingService
    {
        bool TryLoadComplex<TData>(string key, out TData save);
        bool TryLoadSimple<TData>(string key, out TData save);
        void SaveComplex<TData>(string key, TData data);
        bool SaveSimple<TData>(string key, TData save);
    }
}