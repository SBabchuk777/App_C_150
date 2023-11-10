using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class PassButton : MonoBehaviour
    {
        [SerializeField] private BetSelector _betSelector = null;
        [SerializeField] private ResultPanel _resultPanel = null;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (_betSelector.CurrentBet != 0)
                {
                    if (!Wallet.TryPurchase(_betSelector.CurrentBet))
                    {
                        Wallet.TryPurchase(Wallet.Money);
                    }

                    _resultPanel.ShowResults("You lose!", $"-{_betSelector.CurrentBet} coins");
                }
            });
        }
    }
}