using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Basic script to handle collisions with the player
    private void OnCollisionEnter(Collision collision)
    {
        // Perform death logic here
        if (collision.collider.tag == "MainSphere")
        {
            Debug.Log("Player is DEAD!");
            AudioManager.Instance.PlayDeathSFX();
        }
    }
}