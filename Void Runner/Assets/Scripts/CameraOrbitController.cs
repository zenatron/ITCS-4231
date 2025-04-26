using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraOrbitController : MonoBehaviour
{
    [Header("Assign the CinemachineCamera that has OrbitalFollow + RotationComposer")]
    [SerializeField] private CinemachineCamera cineCam;

    [Header("Zoom Settings")]
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 20f;
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineInputAxisController inputController;
    private float initialRadius;

    private void Awake()
    {
        if (!cineCam) {
            Debug.LogError("No CinemachineCamera assigned!");
            return;
        }

        orbitalFollow = cineCam.GetComponent<CinemachineOrbitalFollow>();
    }
    
    private void Start()
    {
        if (orbitalFollow)
        {
            initialRadius = orbitalFollow.Radius;
            // maxRadius no greater than the initial radius
            maxRadius = Mathf.Min(maxRadius, initialRadius);
        }

        if (inputController == null)
            inputController = cineCam.GetComponent<CinemachineInputAxisController>();

        // Start with camera movement disabled until player holds LMB.
        //inputController.enabled = true;
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        // Haven't figured out an elegant way to do this with the new Input System yet
        // Read mouse scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Adjust the radius (distance from target)
            float newRadius = orbitalFollow.Radius - scroll * scrollSensitivity;
            
            // Clamp between minimum and the initial radius (or user-set max if smaller)
            float effectiveMaxRadius = Mathf.Min(maxRadius, initialRadius);
            newRadius = Mathf.Clamp(newRadius, minRadius, effectiveMaxRadius);
            
            orbitalFollow.Radius = newRadius;
        }
    }
}
