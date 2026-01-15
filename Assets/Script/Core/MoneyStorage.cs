using System;

namespace Script.Core
{
    public class MoneyStorage
    {
        public event Action<int> OnMoneyChanged; 

        private int _money;

        public int Money => _money;

        public MoneyStorage(int startingAmount)
        {
            _money = startingAmount;
        }

        public void Earn(int amount)
        {
            if (amount <= 0) return;
            _money += amount;
            OnMoneyChanged?.Invoke(_money);
        }

        public void Spend(int amount)
        {
            if (amount <= 0 || amount > _money) return;
            _money -= amount;
            OnMoneyChanged?.Invoke(_money);
        }

        public bool CanAfford(int amount)
        {
            return _money >= amount;
        }
    }
}