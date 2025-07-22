namespace Infrastructure.IdGenerator
{
    public sealed class UniqueIdGenerator : IUniqueIdGenerator
    {
        private uint _lastGeneratedId;
        
        public uint GetUniqueId()
        {
            return _lastGeneratedId++;
        }
    }
}