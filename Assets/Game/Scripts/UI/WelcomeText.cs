using UnityEngine;
using UnityEngine.UI;
using UserProfile;

namespace UI
{
    public class WelcomeText : MonoBehaviour
    {
        [SerializeField] private Text _text = null;
        
        private void Awake()
        {
            UserProfileStorage.OnChangedUserName += UpdateText;

            UpdateText(UserProfileStorage.UserName);
        }

        private void OnDestroy()
        {
            UserProfileStorage.OnChangedUserName -= UpdateText;
        }

        private void UpdateText(string name) =>
            _text.text = $"Welcome, {name}, to  our app!";
    }
}