using DG.Tweening;
using Infrastructure;
using UnityEngine;

namespace Environment
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;

        private int _invokeEventCount;
        private Vector3 _startPos;
        private void Awake()
        {
            _invokeEventCount = 0;
            _spawner.Spawn += Move;
            _startPos = transform.position;
        }

        public void RestartPosition()
        {
            transform.position = _startPos;
            _invokeEventCount = 0;
        }

        private void Move(float yOffset)
        {
            if (_invokeEventCount == 0)
            {
                _invokeEventCount++;
                return;
            }
            transform.DOMove(transform.position + Vector3.up * yOffset, 1);
        }
    }
}
