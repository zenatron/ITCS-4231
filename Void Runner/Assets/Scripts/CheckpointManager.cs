using UnityEngine;
using System.Collections.Generic;

/// <summary>checkpoint manager</summary>
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

    /// <summary>player transform reference</summary>
    [SerializeField] private Transform playerTransform;
    private Rigidbody rb;

    /// <summary>dictionary of keyboard keys and checkpoint positions for cheat codes</summary>
    private Dictionary<KeyCode, Vector3> _checkpointKeys;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _checkpointKeys = new Dictionary<KeyCode, Vector3>();
    }
    
    /// <summary>checkpoint setup on start</summary>
    private void Start()
    {
        rb = playerTransform.GetComponent<Rigidbody>();
        // Reset checkpoint state
        checkpointState.lastCheckpointID = 0;
        checkpointState.lastCheckpointPosition = playerTransform.position;
        
        // Find and register all checkpoints in the scene on startup
        RegisterAllCheckpoints();
        // Show only the next checkpoint
        UpdateCheckpointVisibility();

        // up to 9 checkpoints for now
        int maxKeys = Mathf.Min(checkpoints.Count, 9);
        for (int i = 0; i < maxKeys; i++)
        {
            _checkpointKeys.Add(KeyCode.Alpha1 + i, checkpoints[i].transform.position);
            Debug.Log("Checkpoint Cheat: " + (i + 1) + " -> " + checkpoints[i].transform.position);
        }
    }

    private void Update() {
        // Check for cheat codes
        foreach (var kv in _checkpointKeys)
        {
            if (Input.GetKeyDown(kv.Key))
            {
                TeleportTo(kv.Value);
                Debug.Log("Teleporting to checkpoint: " + kv.Value);
                break;
            }
        }
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

    public int GetCurrentCheckpoint()
    {
        return checkpointState.lastCheckpointID;
    }

    public void RespawnPlayer() {
        TeleportTo(GetRespawnPosition());
    } 

    /// <summary>teleport player to location</summary>
    /// <param name="position">position to teleport player to</param>
    /// <param name="resetMovement">reset movement and velocity</param>
    public void TeleportTo(Vector3 position, bool resetMovement = true)
    {
        playerTransform.position = position;
        if (resetMovement)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}