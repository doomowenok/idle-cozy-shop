using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Services.UI
{
    [CreateAssetMenu(fileName = nameof(UIConfig), menuName = "Configs/UIConfig")]
    public sealed class UIConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<UIViewType, UIData> _uiData;
        public IReadOnlyDictionary<UIViewType, UIData> UIData => _uiData;
    }
}