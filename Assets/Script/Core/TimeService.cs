using System;
using UnityEngine;

namespace Script.Core
{
    public static class TimeService
    {
        public static bool IsAvailable(string key, float cooldownSeconds)
        {
            if (!PlayerPrefs.HasKey(key)) return true;

            var savedTime = PlayerPrefs.GetString(key);
            if (DateTime.TryParse(savedTime, out var lastUsed))
            {
                return (DateTime.Now - lastUsed).TotalSeconds >= cooldownSeconds;
            }

            return true;
        }

        public static void SetLastUsed(string key)
        {
            PlayerPrefs.SetString(key, DateTime.Now.ToString("o"));
        }
    }
}