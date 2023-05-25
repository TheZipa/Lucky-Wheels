using LuckyWheels.Code.Core.Popup;
using LuckyWheels.Code.Core.Settings;
using LuckyWheels.Code.Core.TopPanel;
using UnityEngine;

namespace LuckyWheels.Code.Services.Factories.PersistentEntityFactory
{
    public interface IPersistentEntityFactory
    {
        TopPanelView CreateTopPanelView(Transform parent);
        SettingsView CreateSettingsPanel(Transform parent);
        Popup CreatePopup(Transform parent);
    }
}