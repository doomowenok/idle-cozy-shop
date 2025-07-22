using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Tests.EditMode
{
    public class ValidationTests
    {
        [TestCaseSource(nameof(AllScenePaths))]
        public void AllGameObjectsShouldNotHaveMissingScripts(string scenePath)
        {
            Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
            
            IEnumerable<string> gameObjectsWithMissingScripts = 
                GetAllGameObjects(scene)
                .Where(HasMissingComponent)
                .GroupBy(gameObject => gameObject.name)
                .Select(grouping => $"{grouping.Key} ({grouping.Count()})")
                .ToList();
            
            EditorSceneManager.CloseScene(scene, true);

            gameObjectsWithMissingScripts.Should().BeEmpty();
        }

        private static IEnumerable<string> AllScenePaths()
        {
            return AssetDatabase
                .FindAssets("t:Scene", new[] { "Assets" })
                .Select(AssetDatabase.GUIDToAssetPath);
        }

        private static IEnumerable<GameObject> GetAllGameObjects(Scene scene)
        {
            Queue<GameObject> gameObjectsQueue = new Queue<GameObject>(scene.GetRootGameObjects());

            while (gameObjectsQueue.Count > 0)
            {
                GameObject gameObject = gameObjectsQueue.Dequeue();
                
                yield return gameObject;

                foreach (Transform child in gameObject.transform)
                {
                    gameObjectsQueue.Enqueue(child.gameObject);
                }
            }
        }
        
        private static bool HasMissingComponent(GameObject gameObject) => 
            GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(gameObject) > 0;
    }
}
