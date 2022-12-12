using System;

namespace Infrastructure.Services
{
    public interface ISceneLoader
    {
        public void Load(string sceneName, Action onLoad = null);
        public void CreateSimulationScene();
    }
}
