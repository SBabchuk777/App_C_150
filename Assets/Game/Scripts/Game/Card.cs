using UnityEngine;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;
        [SerializeField] private CardType _type = CardType.Ace;

        public CardType Type => _type;
        
        public void Show()
        {
            _animator.SetTrigger("Show");
        }
    }
}
