using System;
using Script.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.ShopCoins
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Button freeCoinsButton;
        [SerializeField] private Button watchAdButton;
        [SerializeField] private Button buy150Button;
        [SerializeField] private Button buy450Button;
        [SerializeField] private Button buy1000Button;

        [SerializeField] private TextMeshProUGUI freeCoinsTimerText;

        private Coroutine _freeCoinsTimerCoroutine;

        private ShopCoinsController _shopController;
        
        public void Initialize(MoneyStorage moneyStorage,Action onUpdateUI)
        {
            _shopController = new ShopCoinsController(moneyStorage, onUpdateUI);
        }

        private void OnEnable()
        {
            _shopController.OnFreeCoinsCooldownStarted += StartFreeCoinsTimer;
            freeCoinsButton.onClick.AddListener(_shopController.TryGetFreeCoins);
            watchAdButton.onClick.AddListener(_shopController.TryWatchAd);
            buy150Button.onClick.AddListener(_shopController.Buy150);
            buy450Button.onClick.AddListener(_shopController.Buy450);
            buy1000Button.onClick.AddListener(_shopController.Buy1000);
        }

        private void OnDestroy()
        {
            _shopController.OnFreeCoinsCooldownStarted -= StartFreeCoinsTimer;
            freeCoinsButton.onClick.RemoveListener(_shopController.TryGetFreeCoins);
            watchAdButton.onClick.RemoveListener(_shopController.TryWatchAd);
            buy150Button.onClick.RemoveListener(_shopController.Buy150);
            buy450Button.onClick.RemoveListener(_shopController.Buy450);
            buy1000Button.onClick.RemoveListener(_shopController.Buy1000);

        }

        private void StartFreeCoinsTimer()
        {
            _freeCoinsTimerCoroutine = StartCoroutine(UpdateFreeCoinsTimer());
        }

        private System.Collections.IEnumerator UpdateFreeCoinsTimer()
        {
            const string key = "FreeCoins";
            const float cooldown = 60f;

            freeCoinsButton.interactable = false;
            freeCoinsTimerText.gameObject.SetActive(true);

            while (!TimeService.IsAvailable(key, cooldown))
            {
                var savedTime = PlayerPrefs.GetString(key);
                if (DateTime.TryParse(savedTime, out var lastUsed))
                {
                    var elapsed = (DateTime.Now - lastUsed).TotalSeconds;
                    var remaining = Mathf.CeilToInt((float)(cooldown - elapsed));
                    freeCoinsTimerText.text = $"{remaining}с";
                }

                yield return new WaitForSeconds(1f);
            }

            freeCoinsButton.interactable = true;
            freeCoinsTimerText.gameObject.SetActive(false);
            _freeCoinsTimerCoroutine = null;
        }
        
    }
}