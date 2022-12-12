using Baskets;
using UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public interface IGameFactory
    {
        public IHUD HUD { get; }
        public Basket CreateBasket(int basketNumber, BasketSide basketSide, Vector3 initializePosition);
        public Ball CreateBall(Vector3 initializePosition);
        public void CreateHud();
        public PopupText CreatePopupText();
        public void CreateStar(Vector3 position);
    }
}
