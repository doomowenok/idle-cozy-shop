using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Core
{
    public sealed class ShopsProvider : MonoBehaviour
    {
        [SerializeField] private List<ShopViewBase> _shops;
        
        public IReadOnlyList<ShopViewBase> Shops => _shops;
    }
}