using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoading
{
    public sealed class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(
            string sceneName,
            Action<float> onTick = null,
            Action onComplete = null,
            Action onFail = null,
            LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName, mode);

            if (sceneLoading == null)
            {
                Debug.LogError($"Scene {sceneName} loading failed.");
                onFail?.Invoke();
                return;
            }

            while (!sceneLoading.isDone)
            {
                onTick?.Invoke(sceneLoading.progress);
                await UniTask.Yield();
            }
            
            onComplete?.Invoke();
        }
    }
}