using UnityEngine;

namespace LuckyWheels.Code.Core.ScreenViews
{
    public abstract class Screen : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}