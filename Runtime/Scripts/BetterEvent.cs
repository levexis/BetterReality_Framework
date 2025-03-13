using System;
using System.Collections.Generic;
using UnityEngine;

namespace BetterReality.Framework
{
    [CreateAssetMenu(fileName = "EventName", menuName = "BetterReality/BetterEvent", order = 0)]
    public class BetterEvent : ScriptableObject
    {
        private event Action<String> onEventRaised;

        public void Raise(String value)
        {
            onEventRaised?.Invoke(value);
        }

        public void AddListener(Action<String> listener)
        {
            onEventRaised -= listener;
            onEventRaised += listener;
        }

        public void RemoveListener(Action<String> listener)
        {
            onEventRaised -= listener;
        }
    }
}