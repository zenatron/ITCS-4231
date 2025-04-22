using UnityEngine;

/// <summary>
/// checkpoint functionality
/// </summary>
public class Checkpoint : MonoBehaviour
{
    /*
     * Phil 3/16/2025
     * Checkpoint class
     */
    /// <summary>identifier for checkpoints</summary>
    public int checkpointID;
    
    /// <summary>checkpoint triggered by player?</summary>
    private bool isActivated = false;
    
    void Awake()
    {
        // Sanity check
        if (checkpointID == 0)
        {
            Debug.LogError("Checkpoint ID not set for " + gameObject.name);
        }
    }
    
    /// <summary>adds all checkpoints into checkpoint manager</summary>
    void Start()
    {
        // Register with the checkpoint manager
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.RegisterCheckpoint(this);
        }
        else
        {
            Debug.LogError("CheckpointManager instance not found!");
        }
    }
    
    /// <summary>triggers checkpoint activation on collision</summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }
    
    /// <summary>activates checkpoint</summary>
    public void ActivateCheckpoint()
    {
        if (isActivated) return;
        
        isActivated = true;
        CheckpointManager.Instance.SetCheckpoint(this);
        
        AudioManager.Instance.PlayCheckpointSFX();
        Debug.Log("Checkpoint: " + checkpointID + " activated");
    }
    
    /// <summary>controls checkpoint visibility in the game</summary>
    /// <param name="visible">show/hide checkpoint</param>
    public void SetVisible(bool visible)
    {
        // Don't show if already activated
        if (isActivated)
        {
            gameObject.GetComponent<Renderer>().enabled = visible;
            return;
        }
        
        // Show/hide based on parameter
        gameObject.GetComponent<Renderer>().enabled = visible;
        
        if (visible)
        {
            Debug.Log("Checkpoint: " + checkpointID + " is now visible");
        }
    }

    /// <summary>debug for showing checkpoint id above checkpoint</summary>
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, $"ID: {checkpointID}");
    }
}