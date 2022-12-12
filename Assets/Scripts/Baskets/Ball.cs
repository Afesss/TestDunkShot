using UnityEngine;

namespace Baskets
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
        
        public float GetYPosition()
        {
            return transform.position.y;
        }
        
        public void ActivateRigidbody()
        {
            _rigidbody.isKinematic = false;
        }

        public void DisableRigidbody()
        {
            _rigidbody.isKinematic = true;
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        public void SetLocalPos(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}
