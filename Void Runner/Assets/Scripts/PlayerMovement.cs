using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float accelerationForce = 3f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Assign the Main Camera Here")]
    [SerializeField] private Transform mainCameraTransform;

    private bool isFast = false; 
    private bool isSlow = false;

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

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (isFast) {
                ResetSpeed();
                Debug.Log("Reset Speed");
            } else {
                Fast();
                Debug.Log("Fast");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)){
            if (isSlow) {
                ResetSpeed();
                Debug.Log("Reset Speed");
            } else {
                Slow();
                Debug.Log("Slow");
            }
        }


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

    private void Fast() {
        rb.linearDamping = 0f;
        accelerationForce *= 1.5f;
        isFast = true;
        isSlow = false;
    }

    private void Slow() {
        rb.linearDamping = 5f;
        isFast = false;
        isSlow = true;
    }

    private void ResetSpeed() {
        rb.linearDamping = 3f;
        accelerationForce = 3f;
        isFast = false;
        isSlow = false;
    }
}
