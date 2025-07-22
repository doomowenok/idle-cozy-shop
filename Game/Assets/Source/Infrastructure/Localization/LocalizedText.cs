using TMPro;
using UnityEngine;

namespace Infrastructure.Localization
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _text;

        public string Key => _key;
        
        public void LocalizeText(string value)
        {
            _text.text = value;
        }
    }
}