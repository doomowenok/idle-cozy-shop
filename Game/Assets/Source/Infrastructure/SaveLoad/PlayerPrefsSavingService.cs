using System;
using UnityEngine;

namespace Infrastructure.SaveLoad
{
     public sealed class PlayerPrefsSavingService : ISavingService
        {
            public bool TryLoadComplex<TData>(string key, out TData save)
            {
                if (!PlayerPrefs.HasKey(key))
                {
                    save = default;
                    return false;
                }

                string jsonWithSaveData = PlayerPrefs.GetString(key);
                save = JsonUtility.FromJson<TData>(jsonWithSaveData);

                return save != null;
            }

            public bool TryLoadSimple<TData>(string key, out TData save)
            {
                if (!PlayerPrefs.HasKey(key))
                {
                    save = default;
                    return false;
                }

                Type type = typeof(TData);
                save = default;

                if (type == typeof(int))
                {
                    save = (TData)Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(TData));
                }

                if (type == typeof(float))
                {
                    save = (TData)Convert.ChangeType(PlayerPrefs.GetFloat(key), typeof(TData));
                }

                if (type == typeof(string))
                {
                    save = (TData)Convert.ChangeType(PlayerPrefs.GetString(key), typeof(TData));
                }

                return save != null;
            }

            public void SaveComplex<TData>(string key, TData data)
            {
                string jsonWithData = JsonUtility.ToJson(data);
                PlayerPrefs.SetString(key, jsonWithData);
                PlayerPrefs.Save();
            }

            public bool SaveSimple<TData>(string key, TData save)
            {
                Type type = typeof(TData);

                if (type == typeof(int))
                {
                    PlayerPrefs.SetInt(key, (int)Convert.ChangeType(save, typeof(int)));
                }

                if (type == typeof(float))
                {
                    PlayerPrefs.SetFloat(key, (float)Convert.ChangeType(save, typeof(float)));
                }

                if (type == typeof(string))
                {
                    PlayerPrefs.SetString(key, (string)Convert.ChangeType(save, typeof(string)));
                }

                return save != null;
            }
        }
}