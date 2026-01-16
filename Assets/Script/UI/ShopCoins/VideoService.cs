using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Script.UI.ShopCoins
{
    public class VideoService : MonoBehaviour
    {
        public static VideoService instance;

        [SerializeField] private GameObject videoPanel;
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Button closeButton;

        private System.Action<bool> _onComplete;

        public void Initialize()
        {
            instance = this;
        }
        private void Awake()
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            closeButton.onClick.AddListener(CloseVideo);
        }

        public void PlayAd(System.Action<bool> onComplete)
        {
            _onComplete = onComplete;
            videoPanel.SetActive(true);
            videoPlayer.Play();
        }

        private void OnVideoEnd(VideoPlayer source)
        {
            _onComplete?.Invoke(true);
            CloseVideo();
        }

        private void CloseVideo()
        {
            videoPlayer.Stop();
            videoPanel.SetActive(false);
            _onComplete?.Invoke(false);
        }
    }
}