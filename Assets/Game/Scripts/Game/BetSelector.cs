using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BetSelector : MonoBehaviour
    {
        private struct SelectedChipInfo
        {
            public int priceIndex;
            public GameObject gameObject;
        }
        
        [SerializeField] private Image _chipPrefab = null;
        [SerializeField] private Sprite[] _chipSprites = new Sprite[0];

        private bool _isBlockedEditing = false;

        private List<SelectedChipInfo> _selectedChips = new List<SelectedChipInfo>();

        public int CurrentBet { get; set; }

        private void UpdateChips()
        {
            List<int> newChips = CalculateChips(CurrentBet);

            for (int i = 0; i < newChips.Count; i++)
            {
                if (_selectedChips.Count == i)
                {
                    Vector3 lastPosition = _chipPrefab.transform.position;

                    if (_selectedChips.Count > 0)
                    {
                        lastPosition = _selectedChips.Last().gameObject.transform.position;
                    }
                    
                    SummonNewChip(newChips[i], i, lastPosition);
                }
                else
                {
                    if (_selectedChips[i].priceIndex == newChips[i])
                        continue;

                    Vector3 previsiourPosition = _selectedChips[i].gameObject.transform.position;

                    SummonNewChip(newChips[i], i, previsiourPosition);
                }
            }

            while (newChips.Count < _selectedChips.Count)
            {
                int lastIndex = _selectedChips.Count - 1;

                DestroyChip(lastIndex);
            }
        }

        private void SummonNewChip(int chipIndex, int positionIndex, Vector3 spawnPosition)
        {
            Image newChip = Instantiate(_chipPrefab, _chipPrefab.transform.parent);

            newChip.transform.SetSiblingIndex(positionIndex);
            
            newChip.transform.localScale = Vector3.zero;
            newChip.transform.position = spawnPosition;

            newChip.transform.DOScale(1f, 0.15f);

            newChip.sprite = _chipSprites[chipIndex];
            
            newChip.gameObject.SetActive(true);

            SelectedChipInfo chipInfo = new SelectedChipInfo();

            chipInfo.priceIndex = chipIndex;
            chipInfo.gameObject = newChip.gameObject;
            
            _selectedChips.Insert(positionIndex, chipInfo);
        }

        private void DestroyChip(int positionIndex)
        {
            SelectedChipInfo info = _selectedChips[positionIndex];
                
            _selectedChips.RemoveAt(positionIndex);

            info.gameObject.transform.DOKill();
            info.gameObject.transform.DOScale(0f, 0.15f).OnComplete(() =>
            {
                Destroy(info.gameObject);
            });
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
        
        public void AddChip(int chip)
        {
            if (_isBlockedEditing)
                return;
            
            if (Wallet.Money < CurrentBet + chip)
                return;
            
            CurrentBet += chip;

            CurrentBet = Mathf.Clamp(CurrentBet, 0, 4000);

            UpdateChips();
        }

        public void Clear()
        {
            if (_isBlockedEditing)
                return;

            CurrentBet = 0;

            UpdateChips();
        }

        public void ApplyBet()
        {
            _isBlockedEditing = true;
        }
    }
}
