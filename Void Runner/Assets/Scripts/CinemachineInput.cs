using UnityEngine;
using Unity.Cinemachine;

public class CustomCinemachineInput : MonoBehaviour
{
    private void OnEnable()
    {
        // Override Cinemachine's input delegate.
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    private void OnDisable()
    {
        // Reset the delegate.
        CinemachineCore.GetInputAxis = null;
    }

    // Only return nonzero axis values if the left mouse button is held down.
    private float GetInputAxis(string axisName)
    {
        if (Input.GetMouseButton(0))
            return Input.GetAxis(axisName);
        return 0;
    }
}