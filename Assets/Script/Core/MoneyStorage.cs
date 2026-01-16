using System;

namespace Script.Core
{
    public class MoneyStorage
    {
        public event Action<int, int> OnMoneyChanged; 

        private int _money;

        public int Money => _money;

        public MoneyStorage(int startingAmount)
        {
            _money = startingAmount;
        }

        public void Earn(int amount)
        {
            int old = _money;
            _money += amount;
            OnMoneyChanged?.Invoke(_money, amount); 
        }

        public void Spend(int amount)
        {
            int old = _money;
            _money -= amount;
            OnMoneyChanged?.Invoke(_money, -amount);
        }

        public bool CanAfford(int amount)
        {
            return _money >= amount;
        }
    }
}