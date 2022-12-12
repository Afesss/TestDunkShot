using Environment;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsWindow : MonoBehaviour
    {
        [Header("Window")]
        [SerializeField] private GameObject _window;
        [Header("Button")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backButton;

        [Header("Slider")] 
        [SerializeField] private Slider _colorSlider;
        [SerializeField] private Slider _soundSlider;

        [Header("Menu Backgrounds")] 
        [SerializeField] private Image _settingsBack;
        [SerializeField] private Image _pauseBack;
        

        [Header("Game Color")] 
        [SerializeField] private Color _whiteColor;
        [SerializeField] private Color _darkColor;
        
        private GameEnvironment _environment;
        private ISaveLoadService _saveLoadService;
        private ISoundService _sound;

        public void Construct(ISaveLoadService saveLoadService, ISoundService soundService)
        {
            _sound = soundService;
            _saveLoadService = saveLoadService;
            _settingsButton.onClick.AddListener(OpenWindow);
            _backButton.onClick.AddListener(CloseWindow);
            _colorSlider.onValueChanged.AddListener(ChangeColor);
            _soundSlider.onValueChanged.AddListener(ChangeVolume);
        }
        
        public void Activate(GameEnvironment environment)
        {
            _environment = environment;
            ChangeColor(_saveLoadService.Data.BackgroundColorNum);
            _colorSlider.value = _saveLoadService.Data.BackgroundColorNum;
            ChangeVolume(_saveLoadService.Data.MuteSoundValue);
            _soundSlider.value = _saveLoadService.Data.MuteSoundValue;
        }

        public void HideSettingsButton()
        {
            _settingsButton.gameObject.SetActive(false);
        }

        public void ShowSettingsButton()
        {
            _settingsButton.gameObject.SetActive(true);
        }
        
        private void ChangeVolume(float value)
        {
            if (value == 0)
            {
                _sound.MuteSound(true);
                _saveLoadService.SaveMuteSoundValue((int) value);
                return;
            }

            _sound.MuteSound(false);
            _saveLoadService.SaveMuteSoundValue((int) value);
        }

        private void ChangeColor(float value)
        {
            if (value == 0)
            {
                UpdateBackColors(_whiteColor);
                _saveLoadService.SaveBackgroundColor((int)value);
                return;
            }
            UpdateBackColors(_darkColor);
            _saveLoadService.SaveBackgroundColor((int)value);
        }

        private void UpdateBackColors(Color color)
        {
            _environment.SetBackgroundColor(color);
            _settingsBack.color = color;
            _pauseBack.color = color;
        }

        private void OpenWindow()
        {
            _window.SetActive(true);
        }

        private void CloseWindow()
        {
            _window.SetActive(false);
        }
    }
}