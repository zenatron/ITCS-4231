using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int checkpoint;

    void Start ()
    {
        checkpoint = 0;
    }

    // Basic script to handle collisions with the player
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with " + collision.collider.name);

        // Perform death logic here
        if (collision.collider.tag == "MainSphere")
        {
            Debug.Log("Player is DEAD!");
            AudioManager.Instance.PlayDeathSFX();
        }

        if (collision.collider.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint");
        }
    }

    private void onTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint");
            checkpoint++;
        }
    }

    int getCheckpoint ()
    {
        return checkpoint;
    }
}