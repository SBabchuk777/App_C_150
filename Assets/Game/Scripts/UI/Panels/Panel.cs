using UnityEngine;

namespace UI.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        public bool IsActive { get; private set; }

        public void Show()
        {
            if (IsActive)
                return;
            
            IsActive = true;
            
            gameObject.SetActive(true);
            
            _animator.SetBool("IsActive", IsActive);
        }
        
        public void Hide()
        {
            if (!IsActive)
                return;

            IsActive = false;
            
            _animator.SetBool("IsActive", IsActive);
        }

        private void DisablePanel()
        {
            if (!IsActive)
            {
                gameObject.SetActive(false);
            }
        }
    }
}