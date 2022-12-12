using UnityEngine;

namespace Infrastructure.Services
{
    public class AllServices : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Curtain _curtain;
        [SerializeField] private SoundService _soundService;

        public Curtain Curtain => _curtain;
        public ISoundService SoundService => _soundService;
        public ISceneLoader SceneLoader { get; private set; }
        public IGameFactory GameFactory { get; private set; }
        public IAssetProvider AssetProvider { get; private set; }
        public ISaveLoadService SaveLoad { get; private set; }
        
        public void InitializeSceneLoader()
        {
            SceneLoader = new SceneLoader();
        }

        public void Initialize()
        {
            SaveLoad = new SaveLoadService();
            AssetProvider = new AssetProvider();
            GameFactory = new GameFactory(AssetProvider, this, SaveLoad, _soundService);
        }
    }
}
