using Prototype.AudioCore;
using Tools.UnityAdsService.Scripts;
using UI.Panels;
using UnityEngine;

namespace UI
{
    public class RewardedPanel : Panel
    {
        [SerializeField] private UnityAdsButton _button = null;

        private void Awake()
        {
            _button.OnCanGetReward += GiveReward;
        }

        private void GiveReward()
        {
            AudioController.PlaySound("rewarded_coins");
            
            Wallet.AddMoney(100);
            
            Hide();
        }
    }
}
