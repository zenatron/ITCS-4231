using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float accelerationForce = 3f;
    [SerializeField] private float maxSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * accelerationForce, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * accelerationForce, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * accelerationForce, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * accelerationForce, ForceMode.Acceleration);
        }
        //Debug.Log("Player Linear Velocity: " + rb.linearVelocity.magnitude);
    }
}
