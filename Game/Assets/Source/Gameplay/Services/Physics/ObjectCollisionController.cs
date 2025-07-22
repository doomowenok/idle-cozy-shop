using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Services.Physics
{
    public class ObjectCollisionController : IObjectCollisionController
    {
        private readonly Dictionary<int, MonoBehaviour> _registeredObjects = new(128);
        
        public void Register<T>(T instance) where T : MonoBehaviour
        {
            _registeredObjects.Add(instance.gameObject.GetInstanceID(), instance);
        }

        public bool Is<T>(int id) where T : MonoBehaviour
        {
            if (!_registeredObjects.TryGetValue(id, out MonoBehaviour instance))
            {
                return false;
            }
            
            return instance is T;
        }

        public T Get<T>(int id) where T : MonoBehaviour
        {
            return _registeredObjects[id] as T;
        }
    }
}