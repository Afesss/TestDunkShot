using System;
using System.Collections;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrap : MonoInstaller
    {
        [SerializeField] private AllServices _services;
        
        public override void InstallBindings()
        {
            _services.Curtain.Show();
            Container.BindInstance(_services).AsSingle().NonLazy();
        }

        private void Awake()
        {
            _services.InitializeSceneLoader();
            _services.SceneLoader.Load(SceneLoader.InitialScene, InitializeServices);
        }

        private void InitializeServices()
        {
            _services.Initialize();
            StartCoroutine(LoadGameScene());
        }

        private IEnumerator LoadGameScene()
        {
            yield return null;
            _services.SceneLoader.Load(SceneLoader.GameScene);
        }
    }
}