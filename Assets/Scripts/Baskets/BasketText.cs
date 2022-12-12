using System.Collections;
using Infrastructure.Services;
using UI;
using UnityEngine;

namespace Baskets
{
    public class BasketText
    {
        private bool _textShowed;
        private readonly Basket _basket;
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPointsCounter _pointsCounter;

        public BasketText(Basket basket, IGameFactory gameFactory, ICoroutineRunner coroutineRunner,
            IPointsCounter pointsCounter)
        {
            _basket = basket;
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _pointsCounter = pointsCounter;
        }

        public void ShowText()
        {
            if (_textShowed) return;
            _textShowed = true;
            _coroutineRunner.StartCoroutine(StartText());
        }

        private IEnumerator StartText()
        {
            if (_pointsCounter.PointsStreak > 0)
            {
                PopupText popupText = _gameFactory.CreatePopupText();
                string text;
                
                if(_pointsCounter.PointsStreak == 1)
                    text = "Perfect!";  
                else
                    text = $"Perfect X{_pointsCounter.PointsStreak}";
                Vector3 pos = new Vector3(_basket.transform.position.x, _basket.transform.position.y + 0.5f, -2);
                popupText.Activate(pos, text, PopupText.TextColor.Orange);
            }
            else
            {
                SpawnNumberText();
                yield break;
            }
            
            yield return new WaitForSeconds(0.3f);

            SpawnNumberText();
        }

        private void SpawnNumberText()
        {
            PopupText popupText = _gameFactory.CreatePopupText();
            popupText.Activate(_basket.transform.position + Vector3.up  * 0.5f,
                _pointsCounter.AddedPointsCount.ToString(), PopupText.TextColor.Red);
        }
    }
}