using System;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TouchPad : MonoBehaviour, ITouchPad, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Canvas _canvas;
        
        public event Action Touch;
        public event Action Drag;
        public event Action TouchUp;
        public Vector2 DragDirection { get; private set; }
        public float DragAngle { get; private set; }
        public float DragMagnitude { get; private set; }
        private Vector2 _originPos;

        public void OnDrag(PointerEventData eventData)
        {
            var directionVector = _originPos - eventData.position;
            DragDirection = directionVector.normalized;
            DragMagnitude = directionVector.magnitude / _canvas.scaleFactor;
            DragAngle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg * -1;
            Drag?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Touch?.Invoke();
            _originPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchUp?.Invoke();
        }
    }
}