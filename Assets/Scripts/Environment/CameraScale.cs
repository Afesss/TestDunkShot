using UnityEngine;

namespace Environment
{
    public class CameraScale : MonoBehaviour
    {
        private float _startAspect = 1440f / 2560f;

        private float _defaultWidth;

        private void Awake()
        {
            Camera cam = GetComponent<Camera>();
            _defaultWidth = cam.orthographicSize * _startAspect;

            cam.orthographicSize = _defaultWidth / cam.aspect;
        }
    }
}
