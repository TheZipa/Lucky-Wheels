using System;

namespace LuckyWheels.Code.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}