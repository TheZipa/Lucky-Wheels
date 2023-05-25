using System;
using LuckyWheels.Code.Data.Enums;
using UnityEngine;

namespace LuckyWheels.Code.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }
}