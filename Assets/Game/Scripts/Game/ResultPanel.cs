using Prototype.SceneLoaderCore.Helpers;
using UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ResultPanel : Panel
    {
        [SerializeField] private Text _headerText = null;
        [SerializeField] private Text _descriptionText = null;

        public void ShowResults(string header, string description)
        {
            _headerText.text = header;
            _descriptionText.text = description;
            
            Show();
        }
        
        public void PlayNext()
        {
            if (Wallet.Money >= 50)
            {
                SceneLoader.Instance.SwitchToScene("Game");
            }
            else
            {
                SceneLoader.Instance.SwitchToScene("Menu");
            }
        }
    }
}