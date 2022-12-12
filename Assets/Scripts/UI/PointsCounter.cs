using TMPro;
using UnityEngine;

namespace UI
{
    public class PointsCounter : MonoBehaviour, IPointsCounter
    {
        [SerializeField] private TMP_Text _pointsText;

        public int PointsStreak { get; private set; }
        public int AddedPointsCount { get; private set; }

        private int _pointsCount;

        public void Reset()
        {
            _pointsCount = 0;
            PointsStreak = 0;
            _pointsText.text = _pointsCount.ToString();
        }
        
        public void AddPoints(bool doublePoint)
        {
            if (doublePoint)
                PointsStreak++;
            else
                PointsStreak = 0;
            
            AddedPointsCount = 1 + PointsStreak;
            _pointsCount += 1 + PointsStreak;
            _pointsText.text = _pointsCount.ToString();
        }
    }
}