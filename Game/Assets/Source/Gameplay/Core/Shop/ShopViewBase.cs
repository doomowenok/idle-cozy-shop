using UnityEngine;

namespace Gameplay.Core
{
    public class ShopViewBase : MonoBehaviour
    {
        [SerializeField] private int id;
        
        public int ID => id;
        public int InstanceID => GetInstanceID();
    }
}