using System;
using R3;
using UnityEngine;

namespace Gameplay.Core.Selector
{
    public class ShopSelector : MonoBehaviour
    {
        public Camera Camera;
        public LayerMask LayerMask;

        public ReactiveProperty<GameObject> Selected { get; private set; } =
            new ReactiveProperty<GameObject>();

        public GameObject GameObject;
        
        public void Start()
        {
            Selected.Subscribe(a => GameObject = a).AddTo(this);
            
            var clickStream = Observable
                .EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => Input.mousePosition);

            clickStream
                .Chunk(TimeSpan.FromMilliseconds(250))
                .Where(xs => xs.Length >= 2)
                .Subscribe(xs =>
                {
                    Vector3 screenPos = xs[^1];
                    var ray = Camera.ScreenPointToRay(screenPos);

                    RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, 100, LayerMask);

                    if (hit2D)
                    {
                        Selected.Value = hit2D.collider.gameObject;
                        Debug.Log(hit2D.collider.name);
                    }
                })
                .AddTo(this);
        }
    }
}