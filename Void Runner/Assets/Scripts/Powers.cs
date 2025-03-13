using UnityEngine;

public class Powers : MonoBehaviour
{
    private Rigidbody rb;
    private Transform tf;
    public bool invertGravity = false;
    public bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invertGravity) {
            rb.AddForce(-2*Physics.gravity, ForceMode.Acceleration);
        }
        RaycastHit isFloor;
        Vector3 down = tf.TransformDirection(Vector3.down);

        if (Physics.Raycast(tf.position, down, out isFloor, 0.6f)) {
            isGrounded = true;
            Debug.DrawLine(tf.position, isFloor.point, Color.green);
        } else {
            isGrounded = false;
            Debug.DrawLine(tf.position, tf.position + down * 0.5f, Color.red);
        }

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            invertGravity = !invertGravity;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(0, 5, 0, ForceMode.Impulse);
            isGrounded = false;
        }

    }

}