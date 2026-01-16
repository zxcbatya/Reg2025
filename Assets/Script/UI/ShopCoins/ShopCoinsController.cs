using System;
using Script.Core;
using UnityEngine;

namespace Script.UI.ShopCoins
{
    public class ShopCoinsController
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly Action _onUpdateUI;
        public event Action OnFreeCoinsCooldownStarted;

        public ShopCoinsController(MoneyStorage moneyStorage, Action onUpdateUI)
        {
            _moneyStorage = moneyStorage;
            _onUpdateUI = onUpdateUI;
        }

        public void Buy150()
        {
            SimulatePayment(() => _moneyStorage.Earn(100));
        }

        public void Buy450()
        {
            SimulatePayment(() => _moneyStorage.Earn(500));
        }

        public void Buy1000()
        {
            SimulatePayment(() => _moneyStorage.Earn(1000));
        }

        public void TryGetFreeCoins()
        {
            if (TimeService.IsAvailable("FreeCoins", 60))
            {
                _moneyStorage.Earn(100);
                TimeService.SetLastUsed("FreeCoins");
                OnFreeCoinsCooldownStarted?.Invoke();
            }

            _onUpdateUI?.Invoke();
        }

        public void TryWatchAd()
        {
            if (TimeService.IsAvailable("AdCoins", 30))
            {
                VideoService.instance.PlayAd(OnAdCompleted);
            }

            _onUpdateUI?.Invoke();
        }

        private void OnAdCompleted(bool watchedToTheEnd)
        {
            _moneyStorage.Earn(100);
            TimeService.SetLastUsed("AdCoins");

            _onUpdateUI?.Invoke();
        }

        private void SimulatePayment(Action onComplete)
        {
            UIManager.Instance.ShowPaymentOverlay();
            MonoBehaviourHelper.Instance.StartCoroutine(MonoBehaviourHelper.Instance.Delay(2f, () =>
            {
                onComplete?.Invoke();
                UIManager.Instance.HidePaymentOverlay();
                _onUpdateUI?.Invoke();
            }));
        }

    }
}