using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        private IHUD _hUd;

        public void Construct(IHUD hud)
        {
            _hUd = hud;
            _pauseButton.onClick.AddListener(OpenWindow);
            _resumeButton.onClick.AddListener(CloseWindow);
            _mainMenuButton.onClick.AddListener(ToMainMenu);
        }

        private void ToMainMenu()
        {
            _window.SetActive(false);
            Time.timeScale = 1;
            _hUd.SetHUDState(HUD.State.Menu);
        }

        public void HidePauseButton()
        {
            _pauseButton.gameObject.SetActive(false);
        }

        public void ShowPauseButton()
        {
            _pauseButton.gameObject.SetActive(true);
        }
        
        private void OpenWindow()
        {
            _hUd.SetHUDState(HUD.State.Pause);
            _window.SetActive(true);
            Time.timeScale = 0;
        }

        private void CloseWindow()
        {
            _hUd.SetHUDState(HUD.State.Game);
            _window.SetActive(false);
            Time.timeScale = 1;
        }
    }
}