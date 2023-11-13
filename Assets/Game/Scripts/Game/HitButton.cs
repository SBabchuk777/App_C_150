using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class HitButton : MonoBehaviour
    {
        public event Action OnClick = null;
        
        private Button _button = null;
        
        private void Awake()
        {
            InitButton();
        }

        private void InitButton()
        {
            _button = GetComponent<Button>();
                
            _button.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
        }

        public void SetActive(bool active)
        {
            _button.interactable = active;

            if (active)
            {
                _button.transition = Selectable.Transition.SpriteSwap;
            }
            else
            {
                _button.transition = Selectable.Transition.ColorTint;
            }
        }
    }
}