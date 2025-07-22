using UnityEngine;

namespace Infrastructure.Localization
{
    public interface ILocalizationService
    {
        void Initialize();
        void ChangeLanguage(LanguageType language);
        void SubscribeToLocalization(GameObject gameObject);
    }
}