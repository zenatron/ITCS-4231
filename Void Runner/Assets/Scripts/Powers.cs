using UnityEngine;

public enum Power {
    Default,
    Fast,
    Slow,
    Inverted
}

public class Powers : MonoBehaviour
{
    public static Powers Instance {get; private set;}
    private Rigidbody rb;
    private Transform tf;
    public bool isGrounded = true;

    public Power currPower = Power.Default;
    [SerializeField] private float groundCheckDistance = 0.6f;
    private bool groundedDown = false; // Track if grounded from down raycast
    private bool groundedUp = false;   // Track if grounded from up raycast

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // jumping check
        RaycastHit isFloor;

        groundedDown = Physics.Raycast(tf.position, Vector3.down, out isFloor, groundCheckDistance);
        groundedUp = Physics.Raycast(tf.position, Vector3.up, out isFloor, groundCheckDistance);
        
        isGrounded = groundedDown || groundedUp;
    }

    void OnDrawGizmos()
    {
        // Only draw if we have a transform component
        if (tf == null) return;
        
        // Draw ground check rays
        if (groundedDown) {
            Gizmos.color = Color.green;
        } else {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(tf.position, tf.position + Vector3.down * groundCheckDistance);
        
        if (groundedUp) {
            Gizmos.color = Color.green;
        } else {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(tf.position, tf.position + Vector3.up * groundCheckDistance);
    }

    void Update() {
        
        switch (currPower) {
            case Power.Default:
                SetDefaultPhysics();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;

            case Power.Fast:
                FastAbility();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Default;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;
            
            case Power.Slow:
                SlowAbility();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Default;
                }
                break;
            
            case Power.Inverted:
                InvertAbility();
                SetDefaultPhysics();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Default;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;
        }


        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Jump();
        }
    }

    private void Jump() {
        if (groundedDown) {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isGrounded = false;
        } else {
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    private void SetDefaultPhysics() {
        rb.linearDamping = 1f; // mess around with this
        rb.mass = 1f; 
    }
    private void FastAbility() {
        rb.linearDamping = 7f; // mess around with this
        rb.mass = 0.33f;
    }
    private void SlowAbility() {
        rb.linearDamping = 5f; // mess around with this
        rb.mass = 3f;
    }
    private void InvertAbility() {
        rb.AddForce(-2*Physics.gravity, ForceMode.Acceleration);
    }
}