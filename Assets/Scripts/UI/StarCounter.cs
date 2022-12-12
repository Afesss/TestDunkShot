using Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StarCounter : MonoBehaviour, IStarCounter
    {
        [SerializeField] private TMP_Text _starText;

        public int StarCount { get; private set; }
        
        private ISaveLoadService _saveLoadService;

        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            StarCount = saveLoadService.Data.StarCount;
            _starText.text = StarCount.ToString();
        }
        
        public void AddStar()
        {
            StarCount++;
            _starText.text = StarCount.ToString();
            
            _saveLoadService.SaveStars(StarCount);
        }
    }
}