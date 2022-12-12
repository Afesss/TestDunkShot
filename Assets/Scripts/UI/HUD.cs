using System;
using DG.Tweening;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour, IHUD
    {
        [SerializeField] private TouchPad _touchPad;
        [SerializeField] private PointsCounter _pointsCounter;
        [SerializeField] private StarCounter _starCounter;
        [SerializeField] private PauseWindow _pauseWindow;
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private Button _restartButton;

        public event Action Restart;
        public ITouchPad TouchPad => _touchPad;
        public IPointsCounter PointsCounter => _pointsCounter;
        public IStarCounter StarCounter => _starCounter;
        public SettingsWindow Settings => _settingsWindow;

        private bool _isStart;
        
        public void Construct(ISaveLoadService saveLoadService, ISoundService soundService)
        {
            _settingsWindow.Construct(saveLoadService, soundService);
            _starCounter.Construct(saveLoadService);
            _pauseWindow.Construct(this);
            _isStart = true;
            SetHUDState(State.Menu);
            
            _restartButton.onClick.AddListener(()=>
            {
                SetHUDState(State.Menu);
            });
        }

        public void SetHUDState(State state)
        {
            switch (state)
            {
                case State.Menu:
                    ActivateMenuState();
                    break;
                case State.Game:
                    ActivateGameState();
                    break;
                case State.Pause:
                    ActivatePauseState();
                    break;
                case State.GameOver:
                    ActivateGameOverState();
                    break;
            }
        }

        private void ActivateMenuState()
        {
            if(!_isStart)
                Restart?.Invoke();
            _isStart = false;
            _pointsCounter.transform.localScale = Vector3.zero;
            _restartButton.transform.localScale = Vector3.zero;

            _touchPad.Touch += OnTouch;
            _startWindow.ActivateStartText();
            
            _pauseWindow.HidePauseButton();
            _settingsWindow.ShowSettingsButton();
        }

        private void OnTouch()
        {
            _touchPad.Touch -= OnTouch;
            SetHUDState(State.Game);
        }

        private void ActivateGameState()
        {
            if (_pointsCounter.transform.localScale != Vector3.one)
                _pointsCounter.transform.DOScale(Vector3.one, 1);
            
            _startWindow.DoFadeStartText();
            
            _settingsWindow.HideSettingsButton();
            _pauseWindow.ShowPauseButton();
        }

        private void ActivatePauseState()
        {
            _pauseWindow.HidePauseButton();
            _settingsWindow.ShowSettingsButton();
        }

        private void ActivateGameOverState()
        {
            _restartButton.transform.DOScale(Vector3.one, 0.5f);
            
            _pauseWindow.HidePauseButton();
        }
        
        public enum State
        {
            Menu,
            Game,
            Pause,
            GameOver
        }
    }
}