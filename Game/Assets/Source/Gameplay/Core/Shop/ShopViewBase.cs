using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Core
{
    public class ShopViewBase : MonoBehaviour
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("OnPointerClick");
        }
    }
}