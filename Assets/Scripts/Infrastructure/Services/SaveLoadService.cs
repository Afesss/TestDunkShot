using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string GameDataTitle = "GameData";
        public GameData Data { get; private set; }
        
        public SaveLoadService()
        {
            Load();
        }

        public void SaveStars(int starsCount)
        {
            Data.StarCount = starsCount;
            Save();
        }

        public void SaveBackgroundColor(int number)
        {
            Data.BackgroundColorNum = number;
            Save();
        }

        public void SaveMuteSoundValue(int number)
        {
            Data.MuteSoundValue = number;
            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetString(GameDataTitle, JsonUtility.ToJson(Data));
        }
        
        private void Load()
        {
            string text = PlayerPrefs.GetString(GameDataTitle, null);
            if (string.IsNullOrEmpty(text))
            {
                Data = new GameData();
                return;
            }
            Data = JsonUtility.FromJson<GameData>(text);
        }
    }
}

