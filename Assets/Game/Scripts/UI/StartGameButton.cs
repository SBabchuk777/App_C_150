using Prototype.SceneLoaderCore.Helpers;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private Panel _rewardedPanel = null;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (Wallet.Money < 50)
                {
                    _rewardedPanel.Show();
                }
                else
                {
                    SceneLoader.Instance.SwitchToScene("Game");
                }
            });
        }
    }
}
