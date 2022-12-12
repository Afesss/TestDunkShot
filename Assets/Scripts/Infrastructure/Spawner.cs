using System;
using System.Collections.Generic;
using Baskets;
using Infrastructure.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform _initializePosition;
        
        public event Action<float> Spawn;
        public Ball Ball { get; private set; }
        
        private const float SpawnYOffset = 3;
        private const float SpawnRandomOffset = 1.5f;
        private const float SpawnXPosition = 2;

        private int _basketNum;

        private readonly List<Basket> _spawnedBaskets = new List<Basket>();
        private Basket _activeBasket;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Activate()
        {
            SpawnHud();
            SpawnFirstBasket();
            SpawnNextBasket();
            SpawnBall();
        }

        public void Restart()
        {
            Destroy(Ball.gameObject);
            foreach (Basket basket in _spawnedBaskets)
            {
                Destroy(basket.gameObject);
            }

            _basketNum = 0;
            SpawnFirstBasket();
            SpawnNextBasket();
            SpawnBall();
        }

        private void SpawnHud()
        {
            _gameFactory.CreateHud();
        }

        private void SpawnBall()
        {
            Ball = _gameFactory.CreateBall(_initializePosition.position + Vector3.up);
        }

        private void SpawnFirstBasket()
        {
            _activeBasket = SpawnBasket(BasketSide.Left, _initializePosition.position);
            _activeBasket.SmoothScaleToOne();
            _activeBasket.Destroyed += OnDestroyed;
            _spawnedBaskets.Add(_activeBasket);
        }

        private void SpawnNextBasket()
        {
            BasketSide side = _activeBasket.Side == BasketSide.Left ? BasketSide.Right : BasketSide.Left;
            Basket basket = SpawnBasket(side, GetRandomPosition());
            basket.SmoothScaleToOne();
            basket.Activated += OnActivated;
            basket.Destroyed += OnDestroyed;
            _spawnedBaskets.Add(basket);
            float yOffset = basket.GetYPos() - _activeBasket.GetYPos();

            if (!SpawnStar(basket.transform.position + Vector3.up * 0.5f))
            {
                basket.SetRandomRotation();
            }
            
            Spawn?.Invoke(yOffset);
        }

        private Basket SpawnBasket(BasketSide side, Vector3 spawnPos)
        {
            return _gameFactory.CreateBasket(_basketNum++, side, spawnPos);
        }

        private bool SpawnStar(Vector3 pos)
        {
            if (_basketNum % 5 == 0)
            {
                _gameFactory.CreateStar(pos);
                return true;
            }

            return false;
        }

        private void OnDestroyed(Basket basket)
        {
            basket.Destroyed -= OnDestroyed;
            basket.Activated -= OnActivated;
            _spawnedBaskets.Remove(basket);
        }

        private void OnActivated(Basket basket)
        {
            if(basket == _activeBasket)
                return;
            
            _activeBasket.SmoothDestroy();
            _activeBasket = basket;
            SpawnNextBasket();
        }

        private Vector3 GetRandomPosition()
        {
            float yPosition = _activeBasket.transform.position.y
                              + SpawnYOffset
                              + GetRandomOffset();
            float xPosition = _activeBasket.Side == BasketSide.Left ? SpawnXPosition : -SpawnXPosition;
            xPosition += GetRandomOffset();
            
            return new Vector3(xPosition, yPosition, 0);
        }

        private float GetRandomOffset()
        {
            return Random.Range(-SpawnRandomOffset, SpawnRandomOffset);
        }
    }
}