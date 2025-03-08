using UnityEngine;
using Unity.Cinemachine;

public class NewFreeLookController : MonoBehaviour
{
    [Header("Assign the CinemachineCamera that has OrbitalFollow + RotationComposer")]
    [SerializeField] private CinemachineCamera cineCam;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSensitivity = 3f;

    [Header("Zoom Settings")]
    [SerializeField] private float scrollSensitivity = 5f;
    [SerializeField] private float minRadius = 2f;
    [SerializeField] private float maxRadius = 20f;

    // Cached references to the two key components
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineRotationComposer rotationComposer;

    private void Awake()
    {
        if (!cineCam)
        {
            Debug.LogError("No CinemachineCamera assigned!");
            return;
        }

        // Grab the OrbitalFollow and RotationComposer from the same camera
        orbitalFollow = cineCam.GetComponent<CinemachineOrbitalFollow>();
        rotationComposer = cineCam.GetComponent<CinemachineRotationComposer>();

        if (!orbitalFollow)
            Debug.LogError("CinemachineOrbitalFollow not found on this camera!");
        if (!rotationComposer)
            Debug.LogError("CinemachineRotationComposer not found on this camera!");
    }

    private void Update()
    {
        if (!orbitalFollow || !rotationComposer)
            return;

        HandleZoom();
        HandleRotation();
    }

    private void HandleZoom()
    {
        // Read mouse scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            // Adjust the radius (distance from target)
            float newRadius = orbitalFollow.Radius - scroll * scrollSensitivity;
            newRadius = Mathf.Clamp(newRadius, minRadius, maxRadius);
            orbitalFollow.Radius = newRadius;
        }
    }

    private void HandleRotation()
    {
        // Only rotate if left mouse button is held
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSensitivity;
        }
    }
}
