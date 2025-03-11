using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

namespace BetterReality.Events
{
    
}
// this should hopefull prevent previews from getting stuck
public class XRLoaderManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return InitializeXR();
    }

    private IEnumerator InitializeXR()
    {
        Debug.Log("Initializing XR...");

        // Initialize the loader
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Failed to initialize XR Loader.");
            StopAndDeinitializeXR();
            yield break;
        }

        // Start the subsystems
        Debug.Log("Starting XR subsystems...");
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }

    private void OnDisable()
    {
        StopAndDeinitializeXR();
    }

    private void StopAndDeinitializeXR()
    {
        if (XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            // Stop the subsystems
            Debug.Log("Stopping XR subsystems...");
            XRGeneralSettings.Instance.Manager.StopSubsystems();

            // Deinitialize the loader
            Debug.Log("Deinitializing XR Loader...");
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            Debug.Log("XR Loader stopped completely.");
        }
    }
}