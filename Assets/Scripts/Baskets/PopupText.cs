using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Baskets
{
    public class PopupText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private Color _orange;
        [SerializeField] private Color _red;

        private const float FadeOnDuration = 0.4f;
        private const float FadeOffDuration = 0.3f;

        public void Activate(Vector3 startPos, string text, TextColor color)
        {
            switch (color)
            {
                case TextColor.Orange:
                    _tmpText.color = _orange;
                    break;
                case TextColor.Red:
                    _tmpText.color = _red;
                    break;
            }

            transform.position = startPos;
            _tmpText.text = text;
            _tmpText.alpha = 0;
            _tmpText.gameObject.SetActive(true);
            _tmpText.DOFade(1, FadeOnDuration);
            
            transform.DOMove(transform.position + Vector3.up, 1).OnComplete(() =>
            {
                _tmpText.DOFade(0, FadeOffDuration).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }
        
        public enum TextColor
        {
            Orange,
            Red
        }
    }
}
