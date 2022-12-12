using Environment;
using Infrastructure.Services;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private GameEnvironment _environment;
        
        [Inject] private AllServices _services;

        
        private Camera _camera;
        private bool _gameIsActive;
        private IHUD _hUd;
        
        private void Awake()
        {
            _services.SceneLoader.CreateSimulationScene();
            _spawner.Construct(_services.GameFactory);
            _camera = _cameraMovement.GetComponent<Camera>();
        }

        private void Start()
        {
            _spawner.Activate();
            _hUd = _services.GameFactory.HUD;
            _hUd.Settings.Activate(_environment);
            _hUd.Restart += OnRestart;
            _gameIsActive = true;
            _services.Curtain.Hide();
        }

        private void Update()
        {
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (_gameIsActive && 
                _spawner.Ball.GetYPosition() < _camera.transform.position.y -_camera.orthographicSize - 2)
            {
                _gameIsActive = false;
                _hUd.SetHUDState(HUD.State.GameOver);
            }
        }

        private void OnRestart()
        {
            _cameraMovement.RestartPosition();
            _spawner.Restart();
            _hUd.PointsCounter.Reset();
            _gameIsActive = true;
        }
    }
}
