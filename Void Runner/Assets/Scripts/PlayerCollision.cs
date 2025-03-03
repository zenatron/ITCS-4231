using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Basic script to handler collisions with the player
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with " + collision.collider.name);

        // Perform death logic here
        if (collision.collider.tag == "MainSphere")
        {
            Debug.Log("Player is DEAD!");
        }
    }
}