using System;
using System.Collections;
using UnityEngine;

namespace Script.Core
{
    public class MonoBehaviourHelper : MonoBehaviour
    {
        private static MonoBehaviourHelper _instance;

        public static MonoBehaviourHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("MonoBehaviourHelper");
                    _instance = go.AddComponent<MonoBehaviourHelper>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        public IEnumerator Delay(float seconds, Action onComplete)
        {
            yield return new WaitForSeconds(seconds);
            onComplete?.Invoke();
        }
    }
}