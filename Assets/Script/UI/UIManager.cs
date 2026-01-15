using Script.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject panelMain;
        [SerializeField] private GameObject panelSettings;
        [SerializeField] private GameObject panelShopCoins;
        [SerializeField] private GameObject panelShopBackGround;
        [SerializeField] private GameObject paymentOverlay;

        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button levelEditorButton;

        [Header("ShopCredits")] [SerializeField]
        private Button showShopButton;

        [SerializeField] private Button creditsButton;
        [SerializeField] private Button videoAddedMoneyButton;
        [SerializeField] private Button addedMoneyButton;
        [Header("ShopFon")] [SerializeField] private Button buyButton;

        private MoneyStorage _moneyStorage;
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
                Instance = this;
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(Play);
            settingsButton.onClick.AddListener(ShowSettings);
            showShopButton.onClick.AddListener(ShowShopCoins);
            levelEditorButton.onClick.AddListener(OpenLevelEditor);
            exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(Play);
            settingsButton.onClick.RemoveListener(ShowSettings);
            showShopButton.onClick.RemoveListener(ShowShopCoins);
            levelEditorButton.onClick.RemoveListener(OpenLevelEditor);
            exitButton.onClick.RemoveListener(Exit);
        }

        private void Start()
        {
            panelMain.gameObject.SetActive(true);
        }


        private void ShowShopCoins()
        {
            HideAll();
            panelShopCoins.gameObject.SetActive(true);
        }

        private void ShowSettings()
        {
            HideAll();
            panelSettings.gameObject.SetActive(!panelSettings.gameObject.activeSelf);
        }

        private void OpenLevelEditor()
        {
        }

        public void ShowPaymentOverlay()
        {
            paymentOverlay.gameObject.SetActive(true);
        }

        public void HidePaymentOverlay()
        {
            paymentOverlay.gameObject.SetActive(false);
        }

        private void Play()
        {
            HideAll();
        }

        public void BuyItem(int cost)
        {
            if (_moneyStorage.CanAfford(cost))
            {
                _moneyStorage.Spend(cost);
            }
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void HideAll()
        {
            panelMain.gameObject.SetActive(false);
            panelSettings.gameObject.SetActive(false);
            panelShopCoins.gameObject.SetActive(false);
            panelShopBackGround.gameObject.SetActive(false);
        }
    }
}