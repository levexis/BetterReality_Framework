using UnityEngine;

namespace BetterReality.Framework
{
    public class BetterListener : MonoBehaviour
    {
        public BetterEvent betterEvent;
        public UnityEngine.Events.UnityEvent<string> response;

        private void OnEnable()
        {
            betterEvent?.AddListener(OnEventHandler);
        }

        private void OnDisable()
        {
            betterEvent?.AddListener(OnEventHandler);
        }

        private void OnEventHandler(string value)
        {
            // can trigger a unity event or handle here.
            response?.Invoke(value);
        }
    }
}