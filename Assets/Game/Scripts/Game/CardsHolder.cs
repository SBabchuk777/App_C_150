using DG.Tweening;
using Prototype.AudioCore;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Game
{
    public class CardsHolder : MonoBehaviour
    {
        private void OnEnable()
        {
            ShaffleCards();
        }

        private void ShaffleCards()
        {
            int cardsCount = transform.childCount;

            for (int i = 0; i < cardsCount; i++)
            {
                int newIndex = Random.Range(0, cardsCount);

                transform.GetChild(i).SetSiblingIndex(newIndex);
            }
        }

        public void GetCard(Hand hand)
        {
            AudioController.PlaySound("Card");
            
            int cardsCount = transform.childCount;

            Transform selectedCardTransform = transform.GetChild(cardsCount - 1);
            RectTransform selectedCardRectTransform = selectedCardTransform.GetComponent<RectTransform>();
            Card selectedCard = selectedCardTransform.GetComponent<Card>();

            selectedCardTransform.DOLocalMoveY(-125f, 0.25f).SetEase(Ease.InSine).OnComplete(() =>
            {
                selectedCardRectTransform.DOSizeDelta(hand.InnerCardSize, 0.25f);
                
                selectedCard.Show();
                
                hand.AddCard(selectedCard);
            });
        }
    }
}