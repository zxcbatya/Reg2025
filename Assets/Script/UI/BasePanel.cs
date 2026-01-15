using UnityEngine;

namespace Script.UI
{
    public class BasePanel:MonoBehaviour
    {

        public delegate void ShowPanelDelegate();
        public delegate void HidePanelDelegate();
        
        public event ShowPanelDelegate OnShowPanel;
        public event HidePanelDelegate OnHidePanel;

        protected virtual void Show()
        {
            gameObject.SetActive(true);
            OnShowPanel?.Invoke();
        }
        protected virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHidePanel?.Invoke();
        }
    }
}
