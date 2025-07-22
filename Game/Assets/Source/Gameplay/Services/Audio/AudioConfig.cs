using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Services.Audio
{
    [CreateAssetMenu(fileName = nameof(AudioConfig), menuName = "Configs/Audio")]
    public sealed class AudioConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<AudioType, AudioClip> _audio;
        
        public IReadOnlyDictionary<AudioType, AudioClip> Audio => _audio;
    }
}