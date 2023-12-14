using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickListener : MonoBehaviour
    {
        [SerializeField] private string url;
        
        private Button bttn;
        
        private void Awake()
        {
            bttn = GetComponent<Button>();

            AddListener();
        }

        private void AddListener()
        {
            bttn.onClick.AddListener(() =>
            {
                ShowPage.Instance.Load(url);
            });
        }
    }
}
