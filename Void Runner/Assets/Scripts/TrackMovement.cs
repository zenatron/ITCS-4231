using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

        
    }

    private void FixedUpdate ()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.transform.Rotate(0.5f, 0.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.transform.Rotate(-0.5f, 0.0f, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.transform.Rotate(0.0f, 0.0f, -0.5f, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.transform.Rotate(0.0f, 0.0f, 0.5f, Space.Self);
        }
    }
}
