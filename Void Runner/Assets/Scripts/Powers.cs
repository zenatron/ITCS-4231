using UnityEngine;
using UnityEngine.InputSystem;

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
    private float speed;

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
        // ability actions
        bool ability1 = InputSystem.actions.FindAction("Ability1", throwIfNotFound: true).triggered;
        bool ability2 = InputSystem.actions.FindAction("Ability2", throwIfNotFound: true).triggered;
        bool ability3 = InputSystem.actions.FindAction("Ability3", throwIfNotFound: true).triggered;
        //bool ability4 = InputSystem.actions.FindAction("Ability4", throwIfNotFound: true).triggered;

        // jump action
        bool jump = InputSystem.actions.FindAction("Jump", throwIfNotFound: true).triggered;

        switch (currPower) {
            case Power.Default:
                if (ability1) {
                    currPower = Power.Fast;
                }

                if (ability2) {
                    currPower = Power.Inverted;
                }

                if (ability3) {
                    currPower = Power.Slow;
                }
                break;

            case Power.Fast:
                if (ability1) {
                    currPower = Power.Default;
                }

                if (ability2) {
                    currPower = Power.Inverted;
                }

                if (ability3) {
                    currPower = Power.Slow;
                }
                break;
            
            case Power.Slow:
                if (ability1) {
                    currPower = Power.Fast;
                }

                if (ability2) {
                    currPower = Power.Inverted;
                }

                if (ability3) {
                    currPower = Power.Default;
                }
                break;
            
            case Power.Inverted:
                InvertAbility();
                if (ability1) {
                    currPower = Power.Fast;
                }

                if (ability2) {
                    currPower = Power.Default;
                }

                if (ability3) {
                    currPower = Power.Slow;
                }
                break;
        }


        // jumping
        if (jump && isGrounded) {
            Jump();
        }
    }

    private void Jump() {
        if (groundedDown) {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            isGrounded = false;
        } else {
            rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
            isGrounded = false; 
        }
    }
    private void InvertAbility() {
        rb.AddForce(-1*Physics.gravity, ForceMode.Acceleration);
    }
}