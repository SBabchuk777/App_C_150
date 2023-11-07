using Extension;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;

public class Init : MonoBehaviour
{
    [Header("Main scene"), Scene]
    [SerializeField] private string _mainScene;
    
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        
        QualitySettings.vSyncCount = 0;
        
        Application.targetFrameRate = 60;
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        SceneLoader.Instance.SwitchToScene(_mainScene);
    }
}
