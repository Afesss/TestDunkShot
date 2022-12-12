using Baskets;
using Environment;
using UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        public IHUD HUD { get; private set; }
        
        private const string BasketPrefabPath = "Prefabs/Basket";
        private const string BallPrefabPath = "Prefabs/Ball";
        private const string HudPrefabPath = "Prefabs/Hud";
        private const string BasketTextPath = "Prefabs/PopupText";
        private const string StarPath = "Prefabs/Star";

        private readonly IAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISoundService _soundService;


        public GameFactory(IAssetProvider assetProvider, ICoroutineRunner coroutineRunner,
            ISaveLoadService saveLoadService, ISoundService soundService)
        {
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
            _saveLoadService = saveLoadService;
            _soundService = soundService;
        }

        public Basket CreateBasket(int basketNumber, BasketSide basketSide, Vector3 initializePosition)
        {
            GameObject obj = _assetProvider.Instantiate(BasketPrefabPath, initializePosition);
            Basket basket = obj.GetComponent<Basket>();
            basket.Construct(basketNumber, basketSide, HUD.TouchPad, HUD.PointsCounter, this,
            _coroutineRunner, _soundService);
            return basket;
        }

        public Ball CreateBall(Vector3 initializePosition)
        {
            GameObject obj = _assetProvider.Instantiate(BallPrefabPath, initializePosition);
            Ball ball = obj.GetComponent<Ball>();
            return ball;
        }

        public void CreateHud()
        {
            GameObject obj = _assetProvider.Instantiate(HudPrefabPath);
            var hud = obj.GetComponent<HUD>();
            hud.Construct(_saveLoadService, _soundService);
            HUD = hud;
        }

        public PopupText CreatePopupText()
        {
            GameObject obj = _assetProvider.Instantiate(BasketTextPath);
            return obj.GetComponent<PopupText>();
        }

        public void CreateStar(Vector3 position)
        {
            GameObject obj = _assetProvider.Instantiate(StarPath, position);
            obj.GetComponent<Star>().Construct(HUD.StarCounter);
        }
    }
}
