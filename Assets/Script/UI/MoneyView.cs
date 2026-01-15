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
            OnMoneyChanged(_moneyStorage.Money); 
        }

        private void OnMoneyChanged(int amount)
        {
            coinsText.text = FormatCoins(amount);
        }

        private string FormatCoins(int value)
        {
            if (value >= 1000)
                return $"{value / 1000f:F1}k";
            return value.ToString();
        }
    }
}