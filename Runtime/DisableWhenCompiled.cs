using System;
using UnityEngine;

namespace BetterReality
{
    /// <summary>
    /// Disables gameobject when not in editor mode. Needs to be run first to prevent other components running
    /// </summary>
    public class DisableWhenCompiled : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
            {
                // so will never enable if off in editor
                enabled = BetterUtils.IsEditorMode();
            }
        }
    }
}