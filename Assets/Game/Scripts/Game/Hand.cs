using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Vector2 _innerCardSize = new Vector2(77, 109);

        [Space] 
        
        [SerializeField] private Text _pointsText = null;
        
        private List<Card> _innerCards = new List<Card>();
        
        public Vector2 InnerCardSize => _innerCardSize;
        
        public int Points => BlackjackHandCalculator.CalculateHandValue(_innerCards);

        private void Start()
        {
            SetPointsText();
        }

        private void SetPointsText()
        {
            _pointsText.text = Points.ToString();
        }
        
        private void UpdatePointsText()
        {
            int currentText = 0;

            int.TryParse(_pointsText.text, out currentText);

            _pointsText.DOKill();
            _pointsText.DOCounter(currentText, Points, 0.25f);
        }
        
        public void AddCard(Card card)
        {
            card.transform.SetParent(transform);
            
            _innerCards.Add(card);

            UpdatePointsText();
        }
    }
}