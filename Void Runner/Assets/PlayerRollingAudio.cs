using UnityEngine;

public class PlayerRollingAudio : MonoBehaviour
{
    [SerializeField] private float minRollingSpeed = 0.1f;
    private bool onTrack = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (onTrack && rb.linearVelocity.magnitude > minRollingSpeed)
        {
            AudioManager.Instance.StartRollingSFX();
        }
        else
        {
            AudioManager.Instance.StopRollingSFX();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Track")
        {
            onTrack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            onTrack = false;
        }
    }
}
