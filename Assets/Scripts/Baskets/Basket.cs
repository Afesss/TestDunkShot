using System;
using System.Collections;
using DG.Tweening;
using Infrastructure.Services;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Baskets
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private TrajectoryRenderer _trajectoryRenderer;
        [SerializeField] private TorusColliderDetected _torusColliderDetected;
        [SerializeField] private MeshRenderer _torusMeshRender;
        [Header("Audio")] 
        [SerializeField] private AudioClip _activateClip;
        [SerializeField] private AudioClip _shootClip;
        
        public event Action<Basket> Activated;
        public event Action<Basket> Destroyed;

        private const int MinDragDistance = 100;
        private const int RotationAngle = 40;
        private const float ForceModifier = 0.03f;
        private const float RotationSpeed = 20;

        private bool _isActive;

        private readonly Vector3 _ballPosition = new Vector3(0, -0.4f, 0);
        private Tween _tween;
        private Ball _ball;
        private ITouchPad _touchPad;
        private ISoundService _sound;

        public BasketSide Side { get; private set; }


        public void Construct(int basketNumber, BasketSide side, ITouchPad touchPad,
            IPointsCounter pointsCounter, IGameFactory gameFactory, ICoroutineRunner coroutineRunner,
            ISoundService soundService)
        {
            _sound = soundService;
            Side = side;
            _touchPad = touchPad;
            _touchPad.Drag += OnDrag;
            _touchPad.TouchUp += OnTouchUp;
            _torusColliderDetected.Construct(_sound);
            
            if(basketNumber == 0)
                return;

            BasketText basketText = new BasketText(this, gameFactory, coroutineRunner, pointsCounter);
            BasketPoints basketPoints = new BasketPoints(this, pointsCounter, _torusColliderDetected,
                basketText);
        }

        private void OnDestroy()
        {
            _tween?.Kill();
            _touchPad.Drag -= OnDrag;
            _touchPad.TouchUp -= OnTouchUp;
            Destroyed?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isActive) return;
            Activate(other.GetComponent<Ball>());
        }

        public void SmoothDestroy()
        {
            transform.DOScale(0, 0.5f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void SmoothScaleToOne()
        {
            transform.localScale = Vector3.zero;
            
            _tween = transform.DOScale(1.1f, 0.4f).OnComplete(() =>
            {
                _tween = transform.DOScale(1, 0.3f);
            });
        }

        public float GetYPos()
        {
            return transform.position.y;
        }

        public void SetRandomRotation()
        {
            float maxRotationAngle = Side == BasketSide.Left ? -RotationAngle : RotationAngle;
            float zRotation = Random.Range(0, maxRotationAngle);
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        private void Activate(Ball ball)
        {
            _isActive = true;
            _sound.Play(_activateClip);
            _ball = ball;
            _ball.DisableRigidbody();
            _ball.SetParent(transform);
            _ball.SetLocalPos(_ballPosition);
            Activated?.Invoke(this);
            _torusMeshRender.material.color = Color.gray;
            
            ResetRotation();
        }

        private void ResetRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void OnTouchUp()
        {
            if(!_isActive || CheckMinDragDistance()) return;
            _sound.Play(_shootClip);
            _ball.ActivateRigidbody();
            _ball.SetParent(null);
            _ball.AddForce(GetForceVector());
            
            _trajectoryRenderer.EnableLine(false);
            
            StartCoroutine(DisableBasket());
        }

        private void OnDrag()
        {
            if(!_isActive) return;

            Quaternion newRotation = Quaternion.Euler(0, 0, _touchPad.DragAngle);
            if (CheckMinDragDistance())
            {
                _trajectoryRenderer.EnableLine(false);
                newRotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                _trajectoryRenderer.EnableLine(true);
            }
            
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation,
                RotationSpeed * Time.deltaTime);
           
            _trajectoryRenderer.SimulateTrajectory(_ball.gameObject, 
                _ball.transform.position, 
                GetForceVector());
        }

        private IEnumerator DisableBasket()
        {
            yield return new WaitForSeconds(0.1f);
            _isActive = false;
        }

        private bool CheckMinDragDistance()
        {
            return _touchPad.DragMagnitude < MinDragDistance;
        }

        private Vector3 GetForceVector()
        {
            return _touchPad.DragDirection * (_touchPad.DragMagnitude * ForceModifier);
        }
    }
}
