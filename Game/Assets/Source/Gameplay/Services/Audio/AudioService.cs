using Infrastructure.Config;
using UnityEngine;
using VContainer;

namespace Gameplay.Services.Audio
{
    public sealed class AudioService : MonoBehaviour, IAudioService
    {
        [Inject] private readonly IConfigProvider _configProvider;
        
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void ChangeMusicVolume(float volume)
        {
            _musicSource.volume = volume;
        }

        public void ChangeSoundVolume(float volume)
        {
            _soundSource.volume = volume;
        }

        public void PlayPermanently(AudioType type)
        {
            AudioConfig config = _configProvider.GetConfig<AudioConfig>();
            _musicSource.clip = config.Audio[type];
            _musicSource.Play();
        }

        public void PlayOneShot(AudioType type)
        {
            AudioConfig config = _configProvider.GetConfig<AudioConfig>();
            _soundSource.PlayOneShot(config.Audio[type]);
        }
    }
}