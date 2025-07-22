namespace Gameplay.Services.Audio
{
    public interface IAudioService
    {
        void ChangeMusicVolume(float volume);
        void ChangeSoundVolume(float volume);
        void PlayPermanently(AudioType type);
        void PlayOneShot(AudioType type);
    }
}