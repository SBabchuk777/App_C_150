using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class DealButton : MonoBehaviour
    {
        [SerializeField] private BetSelector _betSelector = null;
        [SerializeField] private Animator _uiAnimator = null;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (_betSelector.CurrentBet != 0)
                {
                    _uiAnimator.SetTrigger("StartGame");
                }
            });
        }
    }
}