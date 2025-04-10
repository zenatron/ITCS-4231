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

    void FixedUpdate()
    {
        if (invertGravity) {
            rb.AddForce(-2*Physics.gravity, ForceMode.Acceleration);
        }

        Vector3 down = Vector3.down;

        if (invertGravity) {
            down = Vector3.up;
        }

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
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            invertGravity = !invertGravity;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(Vector3.up * (invertGravity ? -5 : 5), ForceMode.Impulse);
            isGrounded = false;
        }
        
        if (Time.timeScale == 0) {
            invertGravity = false;
        }

    }

}