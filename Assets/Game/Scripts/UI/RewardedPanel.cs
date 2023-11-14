using Prototype.AudioCore;
using Tools.UnityAdsService.Scripts;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RewardedPanel : Panel
    {
        [SerializeField] private Button _button= null;

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            bool isAdReady = CheckIfAdIsReady();

            _button.interactable = isAdReady;
        }

        private bool CheckIfAdIsReady()
        {
            return UnityAdsService.Instance.IsAvailableShow;
        }

        private void ShowAd()
        {
            var listener = UnityAdsService.Instance.ShowRewardedAd();

            listener.OnShowCompleteAds += GiveReward;
        }

        private void GiveReward()
        {
            AudioController.PlaySound("rewarded_coins");
            
            Wallet.AddMoney(100);
            
            Hide();
        }
        
        public void TryShowRewardedAd()
        {
            if (CheckIfAdIsReady())
            {
                ShowAd();
            }
        }
    }
}
