using System;
using Script.Core;

namespace Script.UI.ShopCoins
{
    public class ShopCoinsController
    {
        private readonly MoneyStorage _moneyStorage;
        private readonly Action _onUpdateUI;

        public ShopCoinsController(MoneyStorage moneyStorage, Action onUpdateUI)
        {
            _moneyStorage = moneyStorage;
            _onUpdateUI = onUpdateUI;
        }

        public void Buy100()
        {
            SimulatePayment(() => _moneyStorage.Earn(100));
        }

        public void Buy500()
        {
            SimulatePayment(() => _moneyStorage.Earn(500));
        }

        public void TryGetFreeCoins()
        {
            if (TimeService.IsAvailable("FreeCoins", 60))
            {
                _moneyStorage.Earn(100);
                TimeService.SetLastUsed("FreeCoins");
            }

            _onUpdateUI?.Invoke();
        }

        public void TryWatchAd()
        {
            if (TimeService.IsAvailable("AdCoins", 30))
            {
                // Открыть видео
                VideoService.instance.PlayAd(OnAdCompleted);
            }

            _onUpdateUI?.Invoke();
        }

        private void OnAdCompleted(bool watchedToTheEnd)
        {
            if (watchedToTheEnd)
            {
                _moneyStorage.Earn(100);
                TimeService.SetLastUsed("AdCoins");
            }

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