using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float baseAcceleration = 3f;
    [SerializeField] private float baseMaxSpeed = 10f;
    [SerializeField] private float accelerationForce;
    [SerializeField] private float maxSpeed;

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
        switch (Powers.Instance.currPower) {
            case Power.Fast:
                accelerationForce = baseAcceleration * 2f;
                maxSpeed = baseMaxSpeed * 1.5f;
                break;

            case Power.Slow:
                accelerationForce = baseAcceleration;
                maxSpeed = baseMaxSpeed * 0.5f;
                break;

            default:
                accelerationForce = baseAcceleration;
                maxSpeed = baseMaxSpeed;
                break;
        }
        
        
        
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

        // current velocity vector
        Vector3 currentVelocity = rb.linearVelocity;

        // multiply braking force
        if (Input.GetKey(KeyCode.C))
        {
            rb.AddForce(-currentVelocity * 4 * accelerationForce, ForceMode.Force);
        }
        else
        {
            // calculate target velocity
            rb.AddForce(moveDirection * accelerationForce, ForceMode.Force);
        }

        
    }
}
