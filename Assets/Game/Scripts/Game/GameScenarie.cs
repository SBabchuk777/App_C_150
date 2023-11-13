using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameScenarie : MonoBehaviour
    {
        [SerializeField] private StandButton _standButton = null;
        [SerializeField] private HitButton _hitButton = null;
        
        [Space]
        
        [SerializeField] private BetSelector _betSelector = null;
        
        [Space]
        
        [SerializeField] private CardsHolder _cardsHolder = null;
        
        [Space]
        
        [SerializeField] private Hand _playerHand = null;
        [SerializeField] private Hand _dillerHand = null;
        
        [Space]
        
        [SerializeField] private ResultPanel _resultPanel = null;

        private IEnumerator Start()
        {
            Wallet.TryPurchase(_betSelector.CurrentBet);
            
            SetButtonsActive(false);

            yield return new WaitForSeconds(1f); 

            yield return GiveStartCards();

            yield return new WaitForSeconds(0.5f);

            yield return StartUserStage();

            yield return new WaitForSeconds(0.5f);

            if (_playerHand.Points > 21)
            {
                ShowResult();
                
                yield break;
            }
            
            yield return StartDillerStage();

            yield return new WaitForSeconds(0.5f);
            
            ShowResult();
        }

        private void SetButtonsActive(bool active)
        {
            _standButton.SetActive(active);
            _hitButton.SetActive(active);
        }

        private IEnumerator GiveStartCards()
        {
            _cardsHolder.GetCard(_playerHand);

            yield return new WaitForSeconds(0.75f); 
            
            _cardsHolder.GetCard(_dillerHand);
            
            yield return new WaitForSeconds(0.75f); 

            _cardsHolder.GetCard(_playerHand);
            
            yield return new WaitForSeconds(0.75f); 

            _cardsHolder.GetCard(_dillerHand);
        
            yield return new WaitForSeconds(0.75f); 
        }
        
        private IEnumerator StartUserStage()
        {
            SetButtonsActive(true);

            bool isPressedHit = false;
            bool isPressedStand = false;

            _hitButton.OnClick += () => isPressedHit = true;
            _standButton.OnClick += () => isPressedStand = true;

            while (true)
            {
                if (isPressedHit)
                {
                    isPressedHit = false;
                    
                    SetButtonsActive(false);

                    _cardsHolder.GetCard(_playerHand);
            
                    yield return new WaitForSeconds(0.75f);

                    if (_playerHand.Points >= 21)
                    {
                        break;
                    }
                    else
                    {
                        SetButtonsActive(true); 
                    }
                }

                if (isPressedStand)
                {
                    isPressedStand = false;
                    
                    SetButtonsActive(false);
                    
                    break;
                }
                
                yield return null;
            }
        }
        
        private IEnumerator StartDillerStage()
        {
            while (_dillerHand.Points < _playerHand.Points)
            {
                _cardsHolder.GetCard(_dillerHand);
            
                yield return new WaitForSeconds(0.75f);

                if (_dillerHand.Points >= 21)
                {
                    break;
                }
            }
        }
        
        private void ShowResult()
        {
            if (_playerHand.Points > 21)
            {
                ShowFail();
                
                return;
            }

            if (_dillerHand.Points > 21)
            {
                ShowWin();
                
                return;
            }

            if (_playerHand.Points == _dillerHand.Points)
            {
                ShowDraw();
                
                return;
            }
            
            if (_playerHand.Points < _dillerHand.Points)
            {
                ShowFail();
                
                return;
            }

            if (_playerHand.Points > _dillerHand.Points)
            {
                ShowWin();
                
                return;
            }
        }

        private void ShowWin()
        {
            int winCoins = _betSelector.CurrentBet * 2;
            
            Wallet.AddMoney(winCoins);
            
            _resultPanel.ShowResults("You win!", $"+{winCoins} coins");
        }
        
        private void ShowFail()
        {
            _resultPanel.ShowResults("You lose!", $"- {_betSelector.CurrentBet} coins");
        }
        
        private void ShowDraw()
        {
            int winCoins = _betSelector.CurrentBet;
            
            Wallet.AddMoney(winCoins);

            _resultPanel.ShowResults("Draw!", $"{winCoins} coins");
        }
    }
}