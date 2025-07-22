using UnityEngine;

namespace Gameplay.Services.Physics
{
    public interface IObjectCollisionController
    {
        void Register<T>(T instance) where T : MonoBehaviour;
        bool Is<T>(int id) where T : MonoBehaviour;
        T Get<T>(int id) where T : MonoBehaviour;
    }
}