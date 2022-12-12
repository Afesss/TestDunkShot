namespace UI
{
    public interface IPointsCounter
    {
        public int PointsStreak { get; }
        public int AddedPointsCount { get; }
        public void Reset();
        public void AddPoints(bool doublePoint);
    }
}