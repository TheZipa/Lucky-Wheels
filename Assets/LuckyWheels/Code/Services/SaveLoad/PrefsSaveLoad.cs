using LuckyWheels.Code.Data.Progress;
using LuckyWheels.Code.Extensions;
using LuckyWheels.Code.Services.PersistentProgress;
using UnityEngine;

namespace LuckyWheels.Code.Services.SaveLoad
{
    public class PrefsSaveLoad : ISaveLoad
    {
        private readonly IPersistentProgress _playerProgress;
        private const string ProgressKey = "Progress";

        public PrefsSaveLoad(IPersistentProgress playerProgress) => _playerProgress = playerProgress;
        
        public void SaveProgress() => 
            PlayerPrefs.SetString(ProgressKey, _playerProgress.Progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}