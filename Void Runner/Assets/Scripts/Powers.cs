using UnityEngine;

public enum Power {
    Fast,
    Slow,
    Inverted,
    Normal
}

public class Powers : MonoBehaviour
{
    public static Powers Instance {get; private set;}
    private Rigidbody rb;
    private Transform tf;
    //public bool inverted false;
    public bool isGrounded = true;

    public Power currPower = Power.Normal;
    public Vector3 down = Vector3.down;

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

        if (Physics.Raycast(tf.position, down, out isFloor, 0.6f)) {
            isGrounded = true;
            Debug.DrawLine(tf.position, isFloor.point, Color.green);
        } else {
            isGrounded = false;
            Debug.DrawLine(tf.position, tf.position + down * 0.5f, Color.red);
        }

    }

    void Update() {
        

        switch (currPower) {
            case Power.Normal:
                ResetSpeed();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                    down *= -1;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;

            case Power.Fast:
                Fast();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Normal;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                    down *= -1;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;
            
            case Power.Slow:
                Slow();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Inverted;
                    down *= -1;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Normal;
                }
                break;
            
            case Power.Inverted:
                Invert();
                ResetSpeed();
                if (Input.GetKeyDown(KeyCode.J)) {
                    currPower = Power.Fast;
                }

                if (Input.GetKeyDown(KeyCode.K)) {
                    currPower = Power.Normal;
                    down *= -1;
                }

                if (Input.GetKeyDown(KeyCode.L)) {
                    currPower = Power.Slow;
                }
                break;
        }


        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * (currPower == Power.Inverted ? -5 : 5), ForceMode.Impulse);
            isGrounded = false;
        }
        

    }

    private void Fast() {
        rb.linearDamping = 7f; // mess around with this
        rb.mass = 0.33f;
    }

    private void Invert() {
        rb.AddForce(-2*Physics.gravity, ForceMode.Acceleration);
    }

    private void Slow() {
        rb.linearDamping = 5f; // mess around with this
        rb.mass = 3f;
    }

    private void ResetSpeed() {
        rb.linearDamping = 3f; // mess around with this
        rb.mass = 1f; 
    }

}