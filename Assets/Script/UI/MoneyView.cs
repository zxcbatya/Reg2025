using Script.Core;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;

        private MoneyStorage _moneyStorage;

        public void Init(MoneyStorage storage)
        {
            _moneyStorage = storage;
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            OnMoneyChanged(_moneyStorage.Money, 0);
        }

        private void OnDestroy()
        {
            if (_moneyStorage != null)
                _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int newAmount, int delta)
        {
            coinsText.text = FormatCoins(newAmount);
        }

        private string FormatCoins(int value)
        {
            if (value >= 1000)
                return $"{value / 1000f:F1}k";
            return value.ToString();
        }
    }
}