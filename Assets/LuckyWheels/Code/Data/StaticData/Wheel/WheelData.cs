using UnityEngine;

namespace LuckyWheels.Code.Data.StaticData.Wheel
{
    [CreateAssetMenu(fileName = "Wheel Data", menuName = "Static Data/Wheel Data")]
    public class WheelData : ScriptableObject
    {
        public string Question;
        public bool IsCustom;
        public Sprite Wheel;
        public Sprite ChoicePreview;
        public Sprite Head;
        public WheelSectorData[] WheelSectors;
    }
}