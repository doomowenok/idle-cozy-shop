namespace Infrastructure.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        void SetTimeScale(float timeScale);
    }
}