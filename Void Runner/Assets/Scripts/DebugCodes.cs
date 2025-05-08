using UnityEngine;

public class DebugCodes : MonoBehaviour
{
    private Transform trans;
    private Rigidbody rb;

    void Start()
    {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>check for cheat code combinations</summary>
    void Update()
    {
        // Alt+R: respawn player at last checkpoint
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    /// <summary>respawn player at last checkpoint</summary>
    private void RespawnPlayer()
    {
        trans.position = CheckpointManager.Instance.GetRespawnPosition();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
