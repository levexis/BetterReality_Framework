
namespace BetterReality.Framework
{
    using UnityEngine;
    using System;

    [CreateAssetMenu(fileName = "State", menuName = "BetterReality/BetterState")]
    public class BetterState : ScriptableObject
    {
        [SerializeField] private string currentState;
        
        
        public static event Action<string> OnStateChanged;

        public string CurrentState
        {
            get { return currentState; }
            private set
            {
                if (currentState != value)
                {
                    SetState(value);
                }
            }
        }

        public string OldState { get; private set; }
        
        public void SetState(string newState)
        {
            OldState = currentState;
            CurrentState = newState;
            OnStateChanged?.Invoke(currentState);
        }

        private void OnEnable()
        {
            currentState = "";
            OldState = "";
        }
    }
}