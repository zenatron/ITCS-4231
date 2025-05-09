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

    private InputAction ability1Action;
    private InputAction ability2Action;
    private InputAction ability3Action;
    private InputAction ability4Action;
    private InputAction jumpAction;

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

    private void OnEnable() {
        ability1Action = InputSystem.actions.FindAction("Ability1", throwIfNotFound: true);
        ability2Action = InputSystem.actions.FindAction("Ability2", throwIfNotFound: true);
        ability3Action = InputSystem.actions.FindAction("Ability3", throwIfNotFound: true);
        ability4Action = InputSystem.actions.FindAction("Ability4", throwIfNotFound: true);
        jumpAction = InputSystem.actions.FindAction("Jump", throwIfNotFound: true);
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


        // actions
        switch (currPower) {
            case Power.Default:
                if (ability1Action.triggered) {
                    currPower = Power.Fast;
                }

                if (ability2Action.triggered) {
                    currPower = Power.Inverted;
                }

                if (ability3Action.triggered) {
                    currPower = Power.Slow;
                }
                break;

            case Power.Fast:
                if (ability1Action.triggered) {
                    currPower = Power.Default;
                }

                if (ability2Action.triggered) {
                    currPower = Power.Inverted;
                }

                if (ability3Action.triggered) {
                    currPower = Power.Slow;
                }
                break;
            
            case Power.Slow:
                if (ability1Action.triggered) {
                    currPower = Power.Fast;
                }

                if (ability2Action.triggered) {
                    currPower = Power.Inverted;
                }

                if (ability3Action.triggered) {
                    currPower = Power.Default;
                }
                break;
            
            case Power.Inverted:
                InvertAbility();
                if (ability1Action.triggered) {
                    currPower = Power.Fast;
                }

                if (ability2Action.triggered) {
                    currPower = Power.Default;
                }

                if (ability3Action.triggered) {
                    currPower = Power.Slow;
                }
                break;
        }


        // jumping
        if (jumpAction.triggered && isGrounded) {
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
        rb.AddForce(-1 * Physics.gravity, ForceMode.Acceleration);
    }
}