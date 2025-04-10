using UnityEngine;

public class FrameRateCap : MonoBehaviour
{
    [Header("Frame Rate Cap Settings")]
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool useVSync = false;

    void Start()
    {
        // Set the target frame rate
        Application.targetFrameRate = targetFrameRate;
        if (useVSync)
        {
            // Enable VSync if specified
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            // Disable VSync
            QualitySettings.vSyncCount = 0;
        }
    }
}
