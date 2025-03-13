using System;
using UnityEngine;

namespace BetterReality.Framework
{
    [CreateAssetMenu(fileName = "BetterApp", menuName = "BetterReality/BetterApp", order = 0)]
    public class BetterApp : BetterState
    {
        public BetterPlayer betterPlayer;
        // eg data handler class
        // private DataLogger dataLogger;
        public void OnEnable()
        {
           Debug.Log("Better App initialising... Instantiating application class instances");
           // so appplication manager interacts with ui / monobehaviours
           // these are decoupled like web workers
        }
    }
}