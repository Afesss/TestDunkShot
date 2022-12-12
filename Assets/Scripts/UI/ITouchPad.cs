using System;
using UnityEngine;

namespace UI
{
    public interface ITouchPad
    {
        public event Action Touch;
        public event Action Drag;
        public event Action TouchUp;
        
        public float DragAngle { get; }
        public float DragMagnitude { get; }
        public Vector2 DragDirection { get; }
    }
}