using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float accelerationForce = 3f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Assign the Main Camera Here")]
    [SerializeField] private Transform mainCameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // private void Update()
    // {
    //     MovePlayer();
    // }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow

        // Get camera's forward and right directions
        Vector3 camForward = mainCameraTransform.forward;
        Vector3 camRight = mainCameraTransform.right;

        // ignore y component
        camForward.y = 0;
        camRight.y = 0;

        // create movement direction relative to camera
        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        // calculate target velocity
        rb.AddForce(moveDirection * accelerationForce, ForceMode.Force);
    }
}
