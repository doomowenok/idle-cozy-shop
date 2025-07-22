using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Infrastructure.Localization
{
    [CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = "Configs/Localization")]
    public sealed class LocalizationConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<string, Dictionary<LanguageType, string>> _dictionary;
        public IReadOnlyDictionary<string, Dictionary<LanguageType, string>> Dictionary => _dictionary;
    }
}