using Infrastructure.Services;
using UI;
using UnityEngine;

namespace Baskets
{
    public class BasketPoints
    {
        private bool _pointsAdded;

        private readonly IPointsCounter _pointsCounter;
        private readonly TorusColliderDetected _torusColliderDetected;
        private readonly BasketText _basketText;

        public BasketPoints(Basket basket, IPointsCounter pointsCounter, TorusColliderDetected torusColliderDetected,
            BasketText basketText)
        {
            _pointsCounter = pointsCounter;
            _torusColliderDetected = torusColliderDetected;
            _basketText = basketText;

            basket.Activated += OnActivated;
        }

        private void OnActivated(Basket basket)
        {
            if(_pointsAdded) return;
            _pointsAdded = true;
            
            bool doublePoint = false;
            
            if (!_torusColliderDetected.Detected)
                doublePoint = true;
            
            _pointsCounter.AddPoints(doublePoint);
            _basketText.ShowText();
        }
    }
}
