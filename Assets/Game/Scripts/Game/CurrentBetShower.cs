using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CurrentBetShower : MonoBehaviour
    {
        [SerializeField] private BetSelector _betSelector = null;
        
        [Space]
        
        [SerializeField] private Image _chipPrefab = null;
        [SerializeField] private Sprite[] _chipSprites = new Sprite[0];

        private List<GameObject> _spawnedChips = new List<GameObject>();

        private void OnEnable()
        {
            UpdateChips();
        }

        private void UpdateChips()
        {
            List<int> newChips = CalculateChips(_betSelector.CurrentBet);

            for (int i = 0; i < _spawnedChips.Count; i++)
            {
                Destroy(_spawnedChips[i]);
            }
            
            _spawnedChips = new List<GameObject>();
            
            for (int i = 0; i < newChips.Count; i++)
            {
                Vector3 lastPosition = _chipPrefab.transform.position;

                if (_spawnedChips.Count > 0)
                {
                    lastPosition = _spawnedChips.Last().transform.position;
                }
                    
                SummonNewChip(newChips[i], i, lastPosition);
            }
        }

        private void SummonNewChip(int chipIndex, int positionIndex, Vector3 spawnPosition)
        {
            Image newChip = Instantiate(_chipPrefab, _chipPrefab.transform.parent);

            newChip.transform.position = spawnPosition;

            newChip.sprite = _chipSprites[chipIndex];
            
            newChip.gameObject.SetActive(true);

            _spawnedChips.Add(newChip.gameObject);
        }

        private List<int> CalculateChips(int amount)
        {
            List<int> chips = new List<int>();
            int[] availableChips = new int[] { 2000, 1000, 500, 100, 50 };
        
            for (int i = 0; i < availableChips.Length; i++)
            {
                while (amount >= availableChips[i])
                {
                    amount -= availableChips[i];
                    
                    chips.Add(Array.IndexOf(availableChips, availableChips[i]));
                }
            }

            return chips;
        }
    }
}
