using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TrackMovement : MonoBehaviour
{
    // private Rigidbody rb;
    /* Phil 3/3/25
    This may change, but unless we need the rigidbody for
    something, it may be better to use transform
    */

    private Transform trans;

    [Header("Rotation Speeds")]
    [SerializeField] public float xSpeed = 0.25f;
    [SerializeField] public float ySpeed = 1f;
    [SerializeField] public float zSpeed = 0.25f;

    
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    private void Update()
    {
        /* Phil 3/3/25
        We probably want an adaptive speed based on the current angular velocity
        Some axes may need their speed to be dampened
        */

        // Pitch (X-axis)
        if (Input.GetKey(KeyCode.W))
        {
            trans.Rotate(-0.5f * xSpeed, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            trans.Rotate(0.5f * xSpeed, 0, 0, Space.Self);
        }

        // Yaw (Y-axis)
        if (Input.GetKey(KeyCode.Q))
        {
            trans.Rotate(0, -0.05f * ySpeed, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            trans.Rotate(0, 0.05f * ySpeed, 0, Space.Self);
        }

        // Roll (Z-axis)
        if (Input.GetKey(KeyCode.A))
        {
            trans.Rotate(0, 0, -0.5f * zSpeed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            trans.Rotate(0, 0, 0.5f * zSpeed, Space.Self);
        }
    }
}