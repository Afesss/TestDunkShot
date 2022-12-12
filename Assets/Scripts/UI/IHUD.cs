using System;

namespace UI
{
    public interface IHUD
    {
        public event Action Restart;
        public ITouchPad TouchPad { get; }
        public IPointsCounter PointsCounter { get; }
        public IStarCounter StarCounter { get; }
        public SettingsWindow Settings { get; }
        public void SetHUDState(HUD.State state);
    }
}