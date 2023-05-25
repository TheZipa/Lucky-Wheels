using UnityEngine;

namespace LuckyWheels.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Lucky Wheels Config", menuName = "Static Data/Lucky Wheels Config")]
    public class LuckyWheelsSettingsConfig : ScriptableObject
    {
        [Range(1, 20)]
        public int SpinDuration;
        public int SpinStatisticCount;
    }
}