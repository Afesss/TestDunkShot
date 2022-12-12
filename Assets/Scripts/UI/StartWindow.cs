using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StartWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _startText;

        public void ActivateStartText()
        {
            _startText.alpha = 1;
        }
        
        public void DoFadeStartText()
        {
            _startText.DOFade(0, 0.5f);
        }
    }
}
