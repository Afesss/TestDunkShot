using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader : ISceneLoader
    {
        public const string SimulationSceneName = "Simulation";
        public const string InitialScene = "Initial";
        public const string GameScene = "Game";
        
        public void Load(string sceneName, Action onLoad = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoad?.Invoke();
                return;
            }
            
            AsyncOperation op =SceneManager.LoadSceneAsync(sceneName);
            op.completed += operation => onLoad?.Invoke();
        }

        public void CreateSimulationScene()
        {
            SceneManager.CreateScene(SimulationSceneName,
                new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        }
    }
}
