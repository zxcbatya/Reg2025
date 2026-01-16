using Script.Core;
using Script.UI.ShopCoins;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject panelMain;
        [SerializeField] private GameObject panelSettings;
        [SerializeField] private GameObject panelShopCoins;
        [SerializeField] private GameObject panelShopBackGround; 
        [SerializeField] private GameObject paymentOverlay;
        
        [SerializeField] private MoneyView creditBuy;
        [SerializeField] private ShopPanel shopPanel;
        [SerializeField] private VideoService videoPanel;


        [Header("Navigation Buttons")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button levelEditorButton;
        [SerializeField] private Button showShopButton;
        
        [Header("Back Buttons")]
        [SerializeField] private Button backCoinsShopButton;

        
        private MoneyStorage _moneyStorage;

        public static UIManager Instance { get; private set; }
        
        public MoneyStorage MoneyStorage => _moneyStorage;

        private void Awake()
        {
            _moneyStorage = new MoneyStorage(1000);
            Instance = this;
            shopPanel.Initialize(_moneyStorage, () => creditBuy.Init(_moneyStorage));
            creditBuy.Init(_moneyStorage);
            videoPanel.Initialize();
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayClicked);
            settingsButton.onClick.AddListener(OnSettingsClicked);
            showShopButton.onClick.AddListener(OnShowShopClicked);
            levelEditorButton.onClick.AddListener(OnLevelEditorClicked);
            exitButton.onClick.AddListener(OnExitClicked);
            backCoinsShopButton.onClick.AddListener(OnBackCoinsShopClicked);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
            showShopButton.onClick.RemoveListener(OnShowShopClicked);
            levelEditorButton.onClick.RemoveListener(OnLevelEditorClicked);
            exitButton.onClick.RemoveListener(OnExitClicked);
        }

        private void Start()
        {
            ShowPanel(panelMain);

        }

        public void ShowPanel(GameObject panel)
        {
            HideAll();
            panel?.SetActive(true);
        }

        public void ShowPaymentOverlay() => paymentOverlay.SetActive(true);
        public void HidePaymentOverlay() => paymentOverlay.SetActive(false);

        private void OnPlayClicked() => HideAll(); // переход потом
        private void OnSettingsClicked() => TogglePanel(panelSettings);
        private void OnShowShopClicked() => ShowPanel(panelShopCoins);
        private void OnLevelEditorClicked() { /* говно */ }
        private void OnExitClicked() => Application.Quit();
        private void OnBackCoinsShopClicked() => ShowPanel(panelMain);

        private void TogglePanel(GameObject panel)
        {
            panel.SetActive(!panel.activeSelf);
        }

        private void HideAll()
        {
            panelMain.SetActive(false);
            panelSettings.SetActive(false);
            panelShopCoins.SetActive(false);
            panelShopBackGround.SetActive(false);
        }
    }
}