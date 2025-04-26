using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Header("Player Movement")]
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
        

        Vector2 movement = InputSystem.actions.FindAction("Move", throwIfNotFound: true).ReadValue<Vector2>();
        float horizontal = movement.x;
        float vertical = movement.y;

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

        // get brake force
        float brakeForce = InputSystem.actions.FindAction("Brake", throwIfNotFound: true).ReadValue<float>();
        
        // apply brake force
        if (brakeForce > 0)
        {
            rb.AddForce(-currentVelocity * 4 * accelerationForce * brakeForce, ForceMode.Force);
        }
        else
        {
            // calculate target velocity
            rb.AddForce(moveDirection * accelerationForce, ForceMode.Force);
        }
    }
}
