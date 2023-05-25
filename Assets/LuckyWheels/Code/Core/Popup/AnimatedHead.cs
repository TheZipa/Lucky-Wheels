using UnityEngine;

namespace LuckyWheels.Code.Core.Popup
{
    public class AnimatedHead : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Show()
        {
            gameObject.SetActive(true);
            _animator.Rebind();
        }

        public void Hide() => gameObject.SetActive(false);
    }
}