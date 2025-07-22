using System;
using System.Collections.Generic;
using Infrastructure.Config;
using Infrastructure.SaveLoad;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;

namespace Infrastructure.Localization
{
    public class LocalizationService : ILocalizationService, IDisposable
    {
        private const string LOCALIZATION_SAVE_KEY = "LastSelectedLanguage";
        
        private readonly ISavingService _savingService;
        private readonly IConfigProvider _configProvider;
        
        private CompositeDisposable _disposables = new CompositeDisposable();

        private readonly HashSet<LocalizedText> _localizedTexts = new HashSet<LocalizedText>(256);
        
        private LocalizationConfig _localizationConfig;

        public ReactiveProperty<LanguageType> Language { get; private set; } =
            new ReactiveProperty<LanguageType>(LanguageType.None);

        public LocalizationService(ISavingService savingService, IConfigProvider configProvider)
        {
            _savingService = savingService;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _localizationConfig = _configProvider.GetConfig<LocalizationConfig>();
            
            Language.Value = _savingService.TryLoadSimple<int>(LOCALIZATION_SAVE_KEY, out var save) 
                ? (LanguageType) save
                : LanguageType.English;

            Language.Subscribe(Save).AddTo(_disposables);
            Language.Subscribe(UpdateLocalizedTexts).AddTo(_disposables);
        }

        private void UpdateLocalizedTexts(LanguageType language)
        {
            _localizedTexts.ForEach(text =>
            {
                text.LocalizeText(GetLocalization(text.Key));
            });
        }

        public void SubscribeToLocalization(GameObject gameObject)
        {
            LocalizedText[] components = gameObject.GetComponents<LocalizedText>();

            foreach (LocalizedText text in components)
            {
                _localizedTexts.Add(text);
            }
        }

        public void ChangeLanguage(LanguageType language)
        {
            Language.Value = language;
        }

        private string GetLocalization(string key)
        {
            if (_localizationConfig.Dictionary.TryGetValue(key, out var localization))
            {
                if (localization.TryGetValue(Language.Value, out var localizedText))
                {
                    return localizedText;
                }
            }

            return "ERROR";
        }

        private void Save(LanguageType language)
        {
            _savingService.SaveSimple<int>(LOCALIZATION_SAVE_KEY, (int)language);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}