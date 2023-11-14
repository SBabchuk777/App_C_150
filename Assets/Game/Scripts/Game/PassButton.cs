using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button))]
    public class PassButton : MonoBehaviour
    {
        [SerializeField] private BetSelector _betSelector = null;
        [SerializeField] private GameScenarie _gameScenarie = null;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                _gameScenarie.PassGame();
            });
        }
    }
}