using UnityEngine;
using Unity.Cinemachine;

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
        if (!cineCam)
        {
            Debug.LogError("No CinemachineCamera assigned!");
            return;
        }

        orbitalFollow = cineCam.GetComponent<CinemachineOrbitalFollow>();

        if (!orbitalFollow)
            Debug.LogError("CinemachineOrbitalFollow not found on this camera!");
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
        inputController.enabled = false;
    }

    private void Update()
    {
        if (!orbitalFollow)
            return;

         // While LMB held, enable input.
        if (Input.GetMouseButtonDown(0))
            inputController.enabled = true;
        
        // While LMB released, disable input.
        if (Input.GetMouseButtonUp(0))
            inputController.enabled = false;

        HandleZoom();
    }

    private void HandleZoom()
    {
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
