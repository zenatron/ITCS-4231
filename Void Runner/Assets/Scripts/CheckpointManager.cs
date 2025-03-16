using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// checkpoint manager
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    /*
    * Phil 3/16/2025
    * This is a singleton class that manages the last checkpoint the player has reached
    */
    /// <summary>singleton instance of the checkpoint manager</summary>
    public static CheckpointManager Instance { get; private set; } 
    
    /// <summary>current checkpoint state data</summary>
    public CheckpointState checkpointState;
    
    /// <summary>list of all checkpoints in the scene</summary>
    private List<Checkpoint> checkpoints = new List<Checkpoint>();
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    /// <summary>checkpoint setup on start</summary>
    private void Start()
    {
        // Reset checkpoint state
        checkpointState.lastCheckpointID = 0;
        checkpointState.lastCheckpointPosition = Vector3.zero;
        
        // Find and register all checkpoints in the scene on startup
        RegisterAllCheckpoints();
        // Show only the next checkpoint
        UpdateCheckpointVisibility();
    }
    
    /// <summary>add checkpoint to list (register)</summary>
    /// <param name="checkpoint">checkpoint to "register"</param>
    public void RegisterCheckpoint(Checkpoint checkpoint)
    {
        if (!checkpoints.Contains(checkpoint))
        {
            checkpoints.Add(checkpoint);
        }
        checkpoints.Sort((a, b) => a.checkpointID.CompareTo(b.checkpointID));
    }
    
    /// <summary>registers all checkpoints in scene</summary>
    private void RegisterAllCheckpoints()
    {
        Checkpoint[] allCheckpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
        foreach (Checkpoint checkpoint in allCheckpoints)
        {
            RegisterCheckpoint(checkpoint);
        }
    }

    /// <summary>set currently active checkpoint</summary>
    /// <param name="checkpoint">checkpoint to set as active</param>
    public void SetCheckpoint(Checkpoint checkpoint)
    {
        checkpointState.lastCheckpointID = checkpoint.checkpointID;
        checkpointState.lastCheckpointPosition = checkpoint.transform.position;
        UpdateCheckpointVisibility();
    }
    
    /// <summary>show only next checkpoint</summary>
    private void UpdateCheckpointVisibility()
    {
        int nextCheckpointID = checkpointState.lastCheckpointID + 1;
        
        foreach (Checkpoint checkpoint in checkpoints)
        {
            // Only make the next checkpoint active
            bool isNextCheckpoint = checkpoint.checkpointID == nextCheckpointID;
            checkpoint.SetVisible(isNextCheckpoint);
        }
    }

    /// <summary>gets vector3 position for player respawn functionality</summary>
    /// <returns>last checkpoint position vector</returns>
    public Vector3 GetRespawnPosition()
    {
        return checkpointState.lastCheckpointPosition;
    }
    
    /// <summary>gets reference to next checkpoint</summary>
    /// <returns>next Checkpoint in sequence or null</returns>
    public Checkpoint GetNextCheckpoint()
    {
        int nextCheckpointID = checkpointState.lastCheckpointID + 1;
        return checkpoints.Find(cp => cp.checkpointID == nextCheckpointID); //yay arrow functions
    }
}