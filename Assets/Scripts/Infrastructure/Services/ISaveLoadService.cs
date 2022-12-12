namespace Infrastructure.Services
{
    public interface ISaveLoadService
    {
        public GameData Data { get; }
        public void SaveStars(int starsCount);
        public void SaveBackgroundColor(int number);
        public void SaveMuteSoundValue(int number);
    }
}

