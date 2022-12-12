using DG.Tweening;
using UI;
using UnityEngine;

namespace Environment
{
    public class Star : MonoBehaviour
    {
        private const float Duration = 0.4f;
        private Vector3 _startPos;
        private Tween _tween;
        private IStarCounter _starCounter;

        public void Construct(IStarCounter starCounter)
        {
            _starCounter = starCounter;
            transform.rotation = Quaternion.Euler(90, 0, 0);
            
            _startPos = transform.position;
            Move();
        }

        private void Move()
        {
            _tween = transform.DOMove(transform.position + Vector3.up * 0.3f, Duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
            {
                _tween = transform.DOMove(_startPos, Duration)
                    .SetEase(Ease.Linear)
                    .OnComplete(Move);
            });
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _starCounter.AddStar();
            _tween.Kill();
            Destroy(gameObject);
        }
    }
}

