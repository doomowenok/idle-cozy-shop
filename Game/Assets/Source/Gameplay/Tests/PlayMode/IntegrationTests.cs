using System.Collections;
using System.Linq;
using FluentAssertions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Gameplay.Tests.PlayMode
{
    public class IntegrationTests
    {
        [UnityTest]
        public IEnumerator When1FramePassed_ThenDeltaTimeChanged()
        {
            // Arrange

            // Act
            yield return null;
            
            // Assert
            Time.deltaTime.Should().BePositive();
        }

        [UnityTest]
        public IEnumerator WhenLoadECSScene_ThenItContainsCamera()
        {
            // Arrange
            
            // Act
            SceneManager.LoadScene("ECS");
            yield return null;
            
            // Assert
            SceneManager
                .GetActiveScene()
                .GetRootGameObjects()
                .Where(x => x.GetComponent<Camera>())
                .Should()
                .NotBeEmpty();
        }
    }
}
