using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    // Reference to the Text Mesh Pro component
    public TextMeshProUGUI fpsText;

    // Update interval in seconds
    public float updateInterval = 0.1f;

    private float accum = 0; // FPS accumulated
    private int frames = 0; // Frames counted
    private int highFps = 0;
    private int lowFps = (int) 10000; // Arbitrary high value

    private void Start()
    {
        // Initialize the FPS text
        fpsText.text = "FPS:0";
    }

    private void Update()
    {
        // Increment frames and accum
        frames++;
        accum += Time.unscaledDeltaTime;

        // Update FPS every interval
        if (accum >= updateInterval)
        {
            // Calculate FPS
            float fps = frames / accum;

            // FPS range
            if (fps > highFps)
            {
                highFps = (int)fps;
            }
            if (fps < lowFps)
            {
                lowFps = (int)fps;
            }

            // Update FPS text
            fpsText.text = $"FPS: {fps:F2}";
            fpsText.text += $"\nHigh: {highFps}";
            fpsText.text += $"\nLow: {lowFps}";

            // Reset accum and frames
            accum = 0;
            frames = 0;
        }
    }
}