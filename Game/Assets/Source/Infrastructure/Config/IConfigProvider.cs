using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Config
{
    public interface IConfigProvider
    {
        TConfig GetConfig<TConfig>() where TConfig : ScriptableObject;
        TConfig GetConfig<TConfig>(string folder) where TConfig : ScriptableObject;
        IEnumerable<TConfig> GetConfigs<TConfig>() where TConfig : ScriptableObject;
        IEnumerable<TConfig> GetConfigs<TConfig>(string folder) where TConfig : ScriptableObject;
    }
}