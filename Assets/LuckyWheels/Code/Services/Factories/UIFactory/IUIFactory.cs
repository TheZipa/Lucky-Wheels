using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.ScreenViews;
using LuckyWheels.Code.Core.Statistics;
using LuckyWheels.Code.Core.Wheel;
using UnityEngine;

namespace LuckyWheels.Code.Services.Factories.UIFactory
{
    public interface IUIFactory
    {
        GameObject CreateRootCanvas();
        MainMenuScreenView CreateMainMenuScreen(Transform parent);
        CreateWheelScreenView CreateWheelScreenView(Transform parent);
        SelectWheelScreenView CreateSelectWheelScreenView(Transform parent);
        SpinWheelScreenView CreateSpinWheelScreenView(Transform parent);
        WheelView CreateWheel(Transform parent);
        AnimatedHead CreateAnimatedHead(Transform parent);
        StatisticsScreen CreateStatisticScreen(Transform parent);
    }
}