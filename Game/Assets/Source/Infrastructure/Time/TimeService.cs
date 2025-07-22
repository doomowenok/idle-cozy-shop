namespace Infrastructure.Time
{
    public sealed class TimeService : ITimeService
    {
        private float _timeScale = 1.0f;
        
        public float DeltaTime => UnityEngine.Time.deltaTime * _timeScale;
        
        public void SetTimeScale(float timeScale)
        {
            _timeScale = timeScale;
        }
    }
}