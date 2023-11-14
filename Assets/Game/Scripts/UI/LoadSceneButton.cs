using Extension;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [Scene]
        [SerializeField] private string _sceneName = "Scene";

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                SceneLoader.Instance.SwitchToScene(_sceneName));
        }
    }
}