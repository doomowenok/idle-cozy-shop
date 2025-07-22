using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoading
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(
            string sceneName, 
            Action<float> onTick = null, 
            Action onComplete = null, 
            Action onFail = null,
            LoadSceneMode mode = LoadSceneMode.Single);
    }
}